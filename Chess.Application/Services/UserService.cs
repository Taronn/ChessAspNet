using Chess.Application.Interfaces.Repositories;
using Chess.Application.Interfaces.Services;

namespace Chess.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public Task<User> FindAsync(int id)
    {
        return _userRepository.FindAsync(id);
    }
}