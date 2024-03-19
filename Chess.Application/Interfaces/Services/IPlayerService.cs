namespace Chess.Application.Interfaces.Services;

public interface IPlayerService
{
    void Join(int playerId);
    public bool IsInGame(int fromId);
}