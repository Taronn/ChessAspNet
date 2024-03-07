using Chess.Application.Interfaces.Caching;
using Chess.Application.Interfaces.Services;

namespace Chess.Application.Services;

public class PlayerService : IPlayerService
{
    private readonly IGameCache _gameCache;
    
    public PlayerService(IGameCache gameCache)
    {
        _gameCache = gameCache;
    }
    
    public void Join(int playerId)
    {
        throw new System.NotImplementedException();
    }
}