namespace Chess.Application.Interfaces.Services;

public interface IGameService
{
    Game Create(Invite invite);
    Game Find(Guid id);
    void Remove(Guid id);
}