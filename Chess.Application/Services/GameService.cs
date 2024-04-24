using Chess.Application.Interfaces.Caching;
using Chess.Application.Interfaces.Repositories;
using Chess.Application.Interfaces.Services;
using Chess.Domain.Enums;

namespace Chess.Application.Services;

public class GameService : IGameService
{
    private readonly IGameCache _gameCache;

    public GameService(IGameCache gameCache)
    {
        _gameCache = gameCache;
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
}