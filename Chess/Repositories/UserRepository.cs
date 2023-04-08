using Chess.Models;
using Microsoft.EntityFrameworkCore;

namespace Chess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChessContext _db;

        public UserRepository(ChessContext db)
        {
            _db = db;
        }

        public async Task CreateUserAsync(User user)
        {
            user.Stats = new Stats() { User = user };
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _db.Users.Include(u => u.Stats).FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _db.Users.Include(u => u.Stats).FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _db.Users.Include(u => u.Stats).FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
