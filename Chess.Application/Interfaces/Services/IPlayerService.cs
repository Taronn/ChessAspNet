namespace Chess.Application.Interfaces.Services;

public interface IPlayerService
{
    Player Find(Guid id);
    Task<Player> Join(Guid playerId, Game game = null);
}