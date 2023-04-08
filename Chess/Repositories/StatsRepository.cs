using Chess.Models;

namespace Chess.Repositories
{
    public class StatsRepository : IStatsRepository
    {
        private readonly ChessContext _db;


        public StatsRepository(ChessContext db)
        {
            _db = db;
        }


        public async Task UpdateStatsAsync(Stats stats)
        {
            _db.Stats.Update(stats);
            await _db.SaveChangesAsync();
        }
    }
}