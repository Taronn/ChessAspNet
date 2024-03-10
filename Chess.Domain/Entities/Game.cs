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
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}