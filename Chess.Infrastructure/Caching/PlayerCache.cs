using System.Collections.Concurrent;
using Chess.Application.Interfaces.Caching;

namespace Chess.Infrastructure.Caching;

public class PlayerCache : IPlayerCache
{
    private readonly ConcurrentDictionary<Guid, Player> _cache = new();

    public Player Find(Guid id)
    {
        return _cache.GetValueOrDefault(id);
    }

    public Player[] FindAll()
    {
        return _cache.Values.ToArray();
    }

    public void Add(Player player)
    {
        _cache[player.Id] = player;
    }
}