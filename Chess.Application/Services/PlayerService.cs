using Chess.Application.Interfaces.Caching;
using Chess.Application.Interfaces.Services;

namespace Chess.Application.Services;

public class PlayerService : IPlayerService
{
    private readonly IPlayerCache _playerCache;
    
    public PlayerService(IPlayerCache playerCache)
    {
        _playerCache = playerCache;
    }
    
    public void Join(int playerId)
    {
        throw new System.NotImplementedException();
    }
    public Player[] GetAllPlayer()
    {
        return _playerCache.FindAll();
    }
}