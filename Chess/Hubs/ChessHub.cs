using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Chess.DTOs;
using Chess.Services;

namespace Chess.Hubs
{
    [Authorize]
    public class ChessHub : Hub
    {
        private PlayerDto Player => _playerService.GetPlayer(Context.UserIdentifier!)!;
        private PlayerDto? Opponent => _gameService.GetOpponent(Player);
        private PlayerDto? Challenger => _challengeService.GetChallenger(Player);
        private GameDto? Game => _gameService.GetGame(Player);
        private ChallengeDto? Challenge => _challengeService.GetChallenge(Player);
        private IEnumerable<PlayerDto> AvailablePlayers => _playerService.GetAvailablePlayers();
        private readonly IGameService _gameService;
        private readonly IPlayerService _playerService;
        private readonly IChallengeService _challengeService;


        public ChessHub(IPlayerService playerService, IGameService gameService, IChallengeService challengeService)
        {
            _playerService = playerService;
            _gameService = gameService;
            _challengeService = challengeService;
        }

        public override async Task OnConnectedAsync()
        {
            string userId = Context.UserIdentifier!;
            GameDto? game = _gameService.GetGameByUserId(userId);
            PlayerDto player = await _playerService.CreatePlayerAsync(userId, game);

            if (game == null)
            {
                await Clients.Users(AvailablePlayers.Where(p => p.Id != Player.Id).Select(p => p.Id)).SendAsync("PlayerJoined", Player);
                await Clients.Caller.SendAsync("GetPlayersList", AvailablePlayers.Where(p => p.Id != Player.Id));
                return;
            }

            await Clients.Caller.SendAsync("SetGame", game.Board.ToPgn(), game.WhitePlayer, game.BlackPlayer);

            if (Opponent == null)
            {
                await Clients.Caller.SendAsync("OpponentDisconnected");
            }
            else
            {
                await Clients.User(Opponent.Id).SendAsync("OpponentConnected");
            }

        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (Opponent != null)
            {
                await Clients.User(Opponent.Id).SendAsync("OpponentDisconnected");
            }

            if (Challenge != null)
            {
                if (Challenger != null)
                {
                    await Clients.User(Challenger.Id).SendAsync("ChallengeError", $"The challenge has expired as the {Player.Username} disconnected from the server.");
                }
                _playerService.RemoveChallenge(Player);
            }

            await Clients.Users(AvailablePlayers.Select(p => p.Id)).SendAsync("PlayerLeft", Player.Id);
            _playerService.RemovePlayer(Player.Id);

            await base.OnDisconnectedAsync(exception);
        }

        public async Task ChallengePlayer(string opponentId, string challengerColor)
        {
            if (opponentId == Context.UserIdentifier)
            {
                await Clients.Caller.SendAsync("ChallengeError", "You cannot challenge yourself to a game. Please select a different opponent.");
            }
            PlayerDto? opponent = _playerService.GetPlayer(opponentId);
            if (opponent == null)
            {
                await Clients.Caller.SendAsync("ChallengeError", "Player is offline. Please try again later or challenge a different player.");
                return;
            }

            ChallengeDto? challenge = _challengeService.CreateChallenge(Player, opponent, challengerColor);

            if (challenge == null)
            {
                await Clients.Caller.SendAsync("PlayerBusy", opponent.Username);
                return;
            }

            await Clients.User(opponent.Id).SendAsync("ChallengeReceived", challenge);
        }

        public async Task AcceptChallenge()
        {
            ChallengeResult result = _challengeService.AcceptChallenge(Player);

            switch (result)
            {
                case ChallengeResult.NoChallengeExists:
                    await Clients.Caller.SendAsync("ChallengeError", "Challenge does not exist.");
                    break;

                case ChallengeResult.ChallengerOffline:
                    await Clients.Caller.SendAsync("ChallengeError", "Challenger is offline.");
                    break;

                case ChallengeResult.ChallengerAlreadyInGame:
                    await Clients.Caller.SendAsync("ChallengeError", "Challenger is already in a game.");
                    break;

                case ChallengeResult.Success:
                    await Clients.Users(Game!.WhitePlayer.Id, Game.BlackPlayer.Id).SendAsync("ChallengeAccepted", Game.Id, Game.WhitePlayer.Username, Game.BlackPlayer.Username);
                    await Clients.Users(AvailablePlayers.Select(p => p.Id)).SendAsync("GameStarted", Game.WhitePlayer, Game.BlackPlayer);
                    break;
            }

        }

        public async Task RejectChallenge()
        {
            await Clients.User(Challenger?.Id ?? "").SendAsync("ChallengeRejected", Player.Username);
            _challengeService.RemoveChallenge(Player);
        }

        private async Task HandleEndGame()
        {
            IClientProxy player = Clients.User(Player.Id);
            IClientProxy opponent = Clients.User(Opponent?.Id ?? "");
            IClientProxy players = Clients.Users(Player.Id, Opponent?.Id ?? "");
            EndgameType? endGameType = await _gameService.EndGameAsync(Player);

            await (endGameType switch
            {
                EndgameType.Checkmate => Task.WhenAll(
                    player.SendAsync("Win", "checkmate"),
                    opponent.SendAsync("Lose", "checkmate")
                ),
                EndgameType.Resigned => Task.WhenAll(
                    opponent.SendAsync("Win", "resignation"),
                    player.SendAsync("Lose", "resignation")
                ),
                EndgameType.DrawDeclared => players.SendAsync("Draw", "draw declaration"),
                EndgameType.Stalemate => players.SendAsync("Draw", "stalemate"),
                EndgameType.FiftyMoveRule => players.SendAsync("Draw", "fifty move rule"),
                EndgameType.InsufficientMaterial => players.SendAsync("Draw", "insufficient material"),
                EndgameType.Repetition => players.SendAsync("Draw", "repetition"),
                _ => Task.CompletedTask
            });
        }

        public async Task MakeMove(string from, string to)
        {
            if (Game == null)
            {
                return;
            }

            MoveResultType moveResult = _gameService.MakeMove(Player, Game, from, to);
            switch (moveResult)
            {
                case MoveResultType.ValidMove:
                    await Clients.User(Opponent?.Id ?? "").SendAsync("MakeMove", from, to);
                    break;
                case MoveResultType.EndGame:
                    await Clients.User(Opponent?.Id ?? "").SendAsync("MakeMove", from, to);
                    await HandleEndGame();
                    break;
                case MoveResultType.InvalidMove:
                    await Clients.Caller.SendAsync("InvalidMove");
                    break;
            }
        }

        public async Task Resign()
        {
            if (_gameService.Resign(Player))
            {
                await HandleEndGame();
            }
        }

        public async Task OfferDraw()
        {
            if (_gameService.OfferDraw(Player))
            {
                await Clients.User(Opponent!.Id).SendAsync("DrawOfferReceived");
            }
        }

        public async Task AcceptDraw()
        {
            if (_gameService.AcceptDraw(Player))
            {
                await HandleEndGame();
            }
        }

        public async Task RejectDraw()
        {
            if (_gameService.RejectDraw(Player))
            {
                await Clients.User(Opponent?.Id ?? "").SendAsync("DrawOfferRejected");
            }
        }
    }
}

