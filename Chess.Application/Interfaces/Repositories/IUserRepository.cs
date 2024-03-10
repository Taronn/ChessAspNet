namespace Chess.Application.Interfaces.Repositories;

public interface IUserRepository
{
    void AddAsync(User user);
    void RemoveAsync(int id);
    Task<User> FindAsync(int id);
    Task<IEnumerable<Player>> AllPlayersAsync();
}