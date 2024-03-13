namespace Chess.Application.Interfaces.Services;

public interface IPlayerService
{
    Player GetPlayer(Guid id);
    void Join(int playerId);
}