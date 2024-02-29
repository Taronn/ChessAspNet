using System.Collections.Concurrent;
using Chess.Application.Interfaces.Caching;

namespace Chess.Infrastructure.Caching;

public class InviteCache : IInviteCache
{
    private readonly ConcurrentDictionary<int, Invite> _cache = new();
    private readonly ConcurrentDictionary<int, Invite> _cacheByPlayer = new();
    
    public Invite FindById(int id)
    {
        return _cache.GetValueOrDefault(id);
    }
    
    public Invite FindByPlayerId(int id)
    {
        return _cache.GetValueOrDefault(id);
    }
    
    public void Add(Invite invite)
    {
        _cache[invite.Id] = invite;
        _cacheByPlayer[invite.ToId] = invite;
    }
}