using System.Collections.Concurrent;
using Chess.Application.Interfaces.Caching;

namespace Chess.Infrastructure.Caching;

public class GameCache : IGameCache
{
    private readonly ConcurrentDictionary<int, Game> _cache = new();
    private readonly ConcurrentDictionary<int, Game> _cacheByPlayer = new();

    public Game FindById(int id)
    {
        return _cache.GetValueOrDefault(id);
    }

    public Game FindByPlayerId(int id)
    {
        return _cacheByPlayer.GetValueOrDefault(id);
    }

    public void Add(Game game)
    {
        _cache[game.Id] = game;
        _cacheByPlayer[game.WhitePlayerId] = game;
        _cacheByPlayer[game.BlackPlayerId] = game;
    }
}