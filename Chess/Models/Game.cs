using System.ComponentModel.DataAnnotations;

namespace Chess.Models
{
    public class Game
    {
        [Key]
        public Guid GameId { get; set; } = new Guid();
        public int? WhitePlayerId { get; set; }
        public User? WhitePlayer { get; set; }
        public int? BlackPlayerId { get; set; }
        public User? BlackPlayer { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public int? WinnerId { get; set; }
        public User? Winner { get; set; }
        public string Pgn { get; set; } = null!;
        public List<Message> GameMessages { get; set; } = new List<Message>();


    }
}