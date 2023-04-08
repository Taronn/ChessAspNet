using Chess.Models;

namespace Chess.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly ChessContext _db;


        public GameRepository(ChessContext db)
        {
            _db = db;
        }


        public async Task CreateGameAsync(Game game)
        {
            await _db.Games.AddAsync(game);
            await _db.SaveChangesAsync();
        }


    }
}
