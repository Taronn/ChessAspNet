using System.Collections.Concurrent;
using Chess.Application.Interfaces.Caching;
using Chess.Domain.Entities;

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
        _cache[game.WhitePlayerId] = game;
        _cache[game.BlackPlayerId] = game;
    }
    public void Remove(Game game)
    {
        _cache.TryRemove(game.WhitePlayerId, out _);
        _cache.TryRemove(game.BlackPlayerId, out _);
    }
}