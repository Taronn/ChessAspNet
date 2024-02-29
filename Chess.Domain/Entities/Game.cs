using Chess.Domain.Enums;


namespace Chess.Domain.Entities
{
    public class Game
    {
        public int Id { get; set; } 
        public GameType Type { get; set; }
        public User WhitePlayer { get; set; }
        public User BlackPlayer { get; set; }
        public User Winner { get; set; }
        public string Pgn { get; set; }
        public int Timer {  get; set; }
        public int TimerIncrement {  get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
