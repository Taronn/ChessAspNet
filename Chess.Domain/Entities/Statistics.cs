using Chess.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Domain.Entities
{
    public class Statistic
    {
        public int Id { get; set; }
        public GameType Type { get; set; }
        public int GamesPlayed { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

    }
}
