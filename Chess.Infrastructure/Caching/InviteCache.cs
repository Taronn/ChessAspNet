using System.Collections.Concurrent;
using Chess.Application.Interfaces.Caching;

namespace Chess.Infrastructure.Caching;

public class InviteCache : IInviteCache
{
    private readonly ConcurrentDictionary<Guid, Invite> _cache = new();

    public Invite Find(Guid id)
    {
        return _cache.GetValueOrDefault(id);
    }
    
    public void Add(Invite invite)
    {
        _cache[invite.ToId] = invite;
    }
}