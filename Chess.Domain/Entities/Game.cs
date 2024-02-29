using Chess.Domain.Enums;


namespace Chess.Domain.Entities;
public class Game : GameConfiguration
{
    public int Id { get; set; }
    public User WhitePlayer { get; set; }
    public int WhitePlayerId { get; set; }
    public User BlackPlayer { get; set; }
    public int BlackPlayerId { get; set; }
    public User Winner { get; set; }
    public int? WinnerId { get; set; }
    public string Pgn { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}