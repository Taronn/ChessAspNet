namespace Chess.Application.Interfaces.Caching;

public interface IInviteCache
{
    Invite FindById(int id);
    Invite FindByPlayerId(int id);
    void Add(Invite invite);
}