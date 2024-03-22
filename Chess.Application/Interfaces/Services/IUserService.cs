namespace Chess.Application.Interfaces.Services;

public interface IUserService
{
    public Task<Player> FindAsync(Guid id);
}