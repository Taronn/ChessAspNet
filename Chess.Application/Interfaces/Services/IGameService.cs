namespace Chess.Application.Interfaces.Services;

public interface IGameService
{
    Game GetGameByUserId(Guid id);
}