using Chess.Domain.Enums;

namespace Chess.Domain.Entities;

public class GameConfiguration
{
    public GameType Type { get; set; }
    public int TypeId
    {
        set => Type = (GameType)value;
    }
    public int Timer { get; set; }
    public int TimerIncrement { get; set; }
}