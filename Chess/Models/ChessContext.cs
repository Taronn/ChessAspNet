using Microsoft.EntityFrameworkCore;

namespace Chess.Models
{

    public partial class ChessContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<Stats> Stats { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;


        public ChessContext(DbContextOptions<ChessContext> options)
                : base(options)
        {
            // Database.EnsureDeleted();

            Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }


    }
}