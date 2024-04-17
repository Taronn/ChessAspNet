using Chess.Domain.Enums;
using System.ComponentModel.DataAnnotations;
namespace Chess.Domain.Entities;

public class GameConfiguration
{
    public GameType Type { get; set; }
    public int TypeId
    {
        set => Type = (GameType)value;
    }
    [Range(1, 60)]
    public int Timer { get; set; }
    [Range(0, 60)]
    public int TimerIncrement { get; set; }
}