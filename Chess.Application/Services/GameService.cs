using Chess.Application.Interfaces.Caching;
using Chess.Application.Interfaces.Repositories;
using Chess.Application.Interfaces.Services;

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
    public Game Add(Game game)
    {
        
        _gameCache.Add(game);
        return game;
    }
}