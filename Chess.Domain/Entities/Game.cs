namespace Chess.Domain.Entities;
public class Game : GameConfiguration
{
    public int Id { get; set; }
    public Player WhitePlayer { get; set; }
    public int WhitePlayerId { get; set; }
    public Player BlackPlayer { get; set; }
    public int BlackPlayerId { get; set; }
    public Player Winner { get; set; }
    public int? WinnerId { get; set; }
    public string Pgn { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}