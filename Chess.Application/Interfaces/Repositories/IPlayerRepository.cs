namespace Chess.Application.Interfaces.Repositories;

public interface IPlayerRepository
{
    void AddAsync(User user);
    Task<Player> FindAsync(Guid id);
}