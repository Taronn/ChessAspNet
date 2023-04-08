using Chess.Repositories;
using Chess.DTOs;
using AutoMapper;
using Chess.Models;

namespace Chess.Services
{
    public class GameService : IGameService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStatsRepository _statsRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GameService(IUserRepository userRepository, IStatsRepository statsRepository, IGameRepository gameRepository, IMapper mapper, ICacheService cacheService)
        {

            _userRepository = userRepository;
            _statsRepository = statsRepository;
            _gameRepository = gameRepository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public GameDto CreateGame(ChallengeDto challenge)
        {
            GameDto game = _mapper.Map<GameDto>(challenge);
            game.WhitePlayer.GameId = game.BlackPlayer.GameId = game.Id;
            _cacheService.AddGame(game);

            return game;
        }

        public GameDto? RemoveGame(PlayerDto player)
        {
            GameDto? game = _cacheService.RemoveGame(player.GameId);
            return game;
        }

        public GameDto? GetGame(PlayerDto player)
        {
            if (player.GameId == Guid.Empty)
            {
                return null;
            }

            return _cacheService.GetGame(player.GameId);
        }


        public GameDto? GetGameByUserId(string id)
        {
            return _cacheService.GetGameByUserId(id);
        }

        public PlayerDto? GetOpponent(PlayerDto player, bool onlineOnly = true)
        {
            GameDto? game = GetGame(player);
            if (game == null)
            {
                return null;
            }
            PlayerDto opponent = game.WhitePlayer == player ? game.BlackPlayer : game.WhitePlayer;
            if (onlineOnly)
            {
                return _cacheService.GetPlayer(opponent.Id);
            }
            return opponent;
        }

        public bool IsPlayerTurn(PlayerDto player, GameDto game)
        {
            return game.Board.Turn == (player == game.WhitePlayer ? PieceColor.White : PieceColor.Black);
        }

        public MoveResultType MakeMove(PlayerDto player, GameDto game, string from, string to)
        {
            Move move = new Move(new Position(from), new Position(to));
            if (IsPlayerTurn(player, game) && game.Board.IsValidMove(move))
            {
                game.Board.Move(move);
                game.IsDrawOffered = false;
                if (game.Board.IsEndGame)
                {
                    return MoveResultType.EndGame;
                }
                return MoveResultType.ValidMove;
            }
            else
            {
                return MoveResultType.InvalidMove;
            }
        }

        private async Task UpdateStats(GameDto game)
        {
            bool? isWhiteWinner, isBlackWinner;
            if (game.Winner == null)
            {
                isWhiteWinner = null;
                isBlackWinner = null;
            }
            else if (game.Winner == game.WhitePlayer)
            {
                isWhiteWinner = true;
                isBlackWinner = false;
            }
            else
            {
                isBlackWinner = true;
                isWhiteWinner = false;
            }

            int oldWhiteRating = game.WhitePlayer.Stats.UpdateStats(game.WhitePlayer.Stats.Rating, isWhiteWinner);
            game.BlackPlayer.Stats.UpdateStats(oldWhiteRating, isBlackWinner);

            await _statsRepository.UpdateStatsAsync(_mapper.Map<Stats>(game.WhitePlayer));
            await _statsRepository.UpdateStatsAsync(_mapper.Map<Stats>(game.BlackPlayer));
        }
        public async Task<EndgameType?> EndGameAsync(PlayerDto player)
        {
            GameDto? game = GetGame(player);
            if (game == null || !game.Board.IsEndGame)
            {
                return null;
            }

            game.EndTime = DateTime.Now;
            EndGameInfo endGame = game.Board.EndGame!;
            if (endGame.EndgameType == EndgameType.Checkmate || endGame.EndgameType == EndgameType.Resigned)
            {
                game.Winner = endGame.WonSide == PieceColor.White ? game.WhitePlayer : game.BlackPlayer;
            }
            else
            {
                game.Winner = null;
            }

            await UpdateStats(game);
            await _gameRepository.CreateGameAsync(_mapper.Map<Game>(game));
            RemoveGame(player);
            return endGame.EndgameType;
        }

        public bool Resign(PlayerDto player)
        {
            GameDto? game = GetGame(player);
            if (game == null)
            {
                return false;
            }
            game.Board.Resign(game.WhitePlayer == player ? PieceColor.White : PieceColor.Black);
            return true;
        }

        public bool OfferDraw(PlayerDto player)
        {
            PlayerDto? opponent = GetOpponent(player);
            GameDto? game = GetGame(player);
            if (game != null && opponent != null && IsPlayerTurn(player, game) && !game.IsDrawOffered)
            {
                game.IsDrawOffered = true;
                return true;
            }
            return false;
        }

        public bool AcceptDraw(PlayerDto player)
        {
            GameDto? game = GetGame(player);
            if (game != null && game.IsDrawOffered)
            {
                game.Board.Draw();
                return true;
            }
            return false;
        }

        public bool RejectDraw(PlayerDto player)
        {
            GameDto? game = GetGame(player);
            if (game != null && game.IsDrawOffered)
            {
                return true;
            }
            return false;
        }


    }

    public enum MoveResultType
    {
        ValidMove,
        InvalidMove,
        EndGame,
    }


}
