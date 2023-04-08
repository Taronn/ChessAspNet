using Chess.DTOs;
using Chess.Models;

namespace Chess.Repositories
{
    public interface IStatsRepository
    {
        Task UpdateStatsAsync(Stats stats);
    }
}
