namespace Chess.Application.Interfaces.Caching;

public interface IInviteCache
{
    Invite Find(Guid id);
    void Add(Invite invite);
}