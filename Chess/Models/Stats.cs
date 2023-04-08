using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chess.Models
{
    public class Stats
    {
        [Key, ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }
        [Required]
        public int GamesPlayed { get; set; } = 0;
        [Required]
        public int Wins { get; set; } = 0;
        [Required]
        public int Losses { get; set; } = 0;
        [Required]
        public int Draws { get; set; } = 0;
        [Required]
        public int Rating { get; set; } = 1200;
        public int WinPercentage => GamesPlayed > 0 ? (int)((double)Wins / GamesPlayed * 100) : 0;


    }
}