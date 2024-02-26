using Chess.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Domain.Entities
{
    public class Stats
    {
        public string Id { get; set; }
        public Gametype Type { get; set; }
        public int GanesPlayed { get; set; }
        public int Wins {  get; set; }
        public int Losses {  get; set; }
        public int Draws { get; set; }
        public int Ratings {  get; set; }
        public DateTime CreateAt { get; set; } 
        public DateTime UpdateAt { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }

    }
}
