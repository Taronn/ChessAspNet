using Chess.Models;

namespace Chess.DTOs
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public PlayerDto WhitePlayer { get; set; } = null!;
        public PlayerDto BlackPlayer { get; set; } = null!;
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; }
        public PlayerDto? Winner { get; set; }
        public bool IsDrawOffered = false;
        public ChessBoard Board { get; set; } = new ChessBoard() { AutoEndgameRules = AutoEndgameRules.All };
        public List<Message> GameMessages { get; set; } = new List<Message>();

    }
}