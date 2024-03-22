namespace Chess.Application.Interfaces.Repositories;

public interface IUserRepository
{
    void AddAsync(User user);
    Task<Player> FindPlayerAsync(Guid id);
}