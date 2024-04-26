using Chess.Application.Interfaces.Caching;
using Chess.Application.Interfaces.Repositories;
using Chess.Application.Interfaces.Services;
using Chess.Domain.Enums;

namespace Chess.Application.Services;

public class GameService : IGameService
{
    private readonly IGameCache _gameCache;
    private readonly IPlayerCache _playerCache;

    public GameService(IGameCache gameCache, IPlayerCache playerCache)
    {
        _gameCache = gameCache;
        _playerCache = playerCache;
    }
    public Game Find(Guid id)
    {
        return _gameCache.Find(id);
    }
    public void Remove(Guid id)
    {
        _gameCache.Remove(id);
    }

    public Game Create(Invite invite)
    {
        Game game = new Game();
        if (invite.FromColor == Color.White)
        {
            game.WhitePlayerId = invite.FromId;
            game.WhitePlayer = invite.From;
            game.BlackPlayerId = invite.ToId;
            game.BlackPlayer = invite.To;
        }
        else
        {
            game.WhitePlayerId = invite.ToId;
            game.WhitePlayer = invite.To;
            game.BlackPlayerId = invite.FromId;
            game.BlackPlayer = invite.From;
        }
        game.Timer = invite.Timer;
        game.TimerIncrement = invite.TimerIncrement;
        game.TypeId = invite.TypeId;
        _gameCache.Add(game);
        return game;
    }
    public bool IsPlayerTurn(Player player, Game game)
    {
        return game.Board.Turn == (player == game.WhitePlayer ? PieceColor.White : PieceColor.Black);
    }
    public Player GetOpponent(Player player, bool onlineOnly = true)
    {
        Game game = _gameCache.Find(player.Id);
        if (game == null)
        {
            return null;
        }
        Player opponent = game.WhitePlayer == player ? game.BlackPlayer : game.WhitePlayer;
        if (onlineOnly)
        {
            return _playerCache.Find(opponent.Id);
        }
        return opponent;
    }
    public MoveResultType MakeMove(Player player, Game game, string from, string to)
    {
        Move move = new Move(new Position(from), new Position(to));
        if (IsPlayerTurn(player, game) && game.Board.IsValidMove(move))
        {
            game.Board.Move(move);
/*            game.IsDrawOffered = false;
*/            if (game.Board.IsEndGame)
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
    public async Task<EndgameType?> EndGameAsync(Player player)
    {
        Game game = _gameCache.Find(player.Id);
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
/*        await _gameRepository.CreateGameAsync(_mapper.Map<Game>(game));
*/        Remove(player.Id);
        return endGame.EndgameType;
    }
    private async Task UpdateStats(Game game)
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

       /* int oldWhiteRating = game.WhitePlayer.Stats.UpdateStats(game.WhitePlayer.Stats.Rating, isWhiteWinner);
        game.BlackPlayer.Stats.UpdateStats(oldWhiteRating, isBlackWinner);

        await _statsRepository.UpdateStatsAsync(_mapper.Map<Stats>(game.WhitePlayer));
        await _statsRepository.UpdateStatsAsync(_mapper.Map<Stats>(game.BlackPlayer));*/
    }
    public bool Resign(Player player)
    {
        Game game = _gameCache.Find(player.Id);
        if (game == null)
        {
            return false;
        }
        game.Board.Resign(game.WhitePlayer == player ? PieceColor.White : PieceColor.Black);
        return true;
    }
    public bool OfferDraw(Player player)
    {
        Player opponent = GetOpponent(player);
        Game game = _gameCache.Find(player.Id);
        /*if (game != null && opponent != null && IsPlayerTurn(player, game) && !game.IsDrawOffered)
        {
            game.IsDrawOffered = true;
            return true;
        }*/
        return false;
    }
    public bool AcceptDraw(Player player)
    {
        Game game = _gameCache.Find(player.Id);
        /*if (game != null && game.IsDrawOffered)
        {
            game.Board.Draw();
            return true;
        }*/
        return false;
    }
    public bool RejectDraw(Player player)
    {
        Game game = _gameCache.Find(player.Id);
        /*if (game != null && game.IsDrawOffered)
        {
            return true;
        }*/
        return false;
    }
    public enum MoveResultType
    {
        ValidMove,
        InvalidMove,
        EndGame,
    }
}