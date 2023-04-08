using Chess.DTOs;
using Chess.Models;

namespace Chess.Repositories
{
    public interface IGameRepository
    {
        Task CreateGameAsync(Game game);
    }
}
