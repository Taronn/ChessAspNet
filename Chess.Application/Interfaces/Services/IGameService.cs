namespace Chess.Application.Interfaces.Services;

public interface IGameService
{
    Game Find(Guid id);
}