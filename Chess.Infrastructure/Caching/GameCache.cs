using System.Collections.Concurrent;
using Chess.Application.Interfaces.Caching;

namespace Chess.Infrastructure.Caching;

public class GameCache : IGameCache
{
    private readonly ConcurrentDictionary<Guid, Game> _cache = new();

    public Game Find(Guid id)
    {
        return _cache.GetValueOrDefault(id);
    }

    public void Add(Game game)
    {
        _cache[game.Id] = game;
        _cache[game.WhitePlayerId] = game;
        _cache[game.BlackPlayerId] = game;
    }
}