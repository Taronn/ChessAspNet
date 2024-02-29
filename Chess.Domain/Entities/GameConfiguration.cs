using Chess.Domain.Enums;

namespace Chess.Domain.Entities;

public class GameConfiguration
{
    public GameType Type { get; set; }
    public int Timer { get; set; }
    public int TimerIncrement { get; set; }
}