using Chess.Application.Interfaces.Caching;
using Chess.Application.Interfaces.Repositories;
using Chess.Application.Interfaces.Services;

namespace Chess.Application.Services;

public class PlayerService : IPlayerService
{
    private readonly IPlayerCache _playerCache;
    private readonly IUserRepository _userRepository;
    public PlayerService(IPlayerCache playerCache, IUserRepository userRepository)
    {
        _playerCache = playerCache;
        _userRepository = userRepository;
    }

    public Player Find(Guid id)
    {
        return _playerCache.Find(id);
    }

    public async Task<Player> Join(Guid playerId)
    {
        Player? player = await _userRepository.FindPlayerAsync(playerId);
        _playerCache.Add(player);
        return player;
    }
    public Player[] FindAll()
    {
        return _playerCache.FindAll();
    }
    public void Remove(Guid id)
    {
        _playerCache.Remove(id);
    }
}