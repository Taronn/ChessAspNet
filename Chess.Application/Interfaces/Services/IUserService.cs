namespace Chess.Application.Interfaces.Services;

public interface IUserService
{
    public Task<User> FindAsync(Guid id);
}