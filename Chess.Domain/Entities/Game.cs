using System.ComponentModel.DataAnnotations;

namespace Chess.Domain.Entities;
public class Game : GameConfiguration
{
    public Guid Id { get; set; }
    public Player WhitePlayer { get; set; }
    public Guid WhitePlayerId { get; set; }
    public Player BlackPlayer { get; set; }
    public Guid BlackPlayerId { get; set; }
    public Player Winner { get; set; }
    public Guid? WinnerId { get; set; }
    public string Pgn { get; set; }
    [Required]
    public DateTime StartTime { get; set; } = DateTime.Now;
    [Required]
    public DateTime EndTime { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public ChessBoard Board { get; set; } = new ChessBoard() { AutoEndgameRules = AutoEndgameRules.All };
}