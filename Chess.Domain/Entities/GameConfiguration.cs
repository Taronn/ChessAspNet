using Chess.Domain.Enums;
using System.ComponentModel.DataAnnotations;
namespace Chess.Domain.Entities;

public class GameConfiguration
{
    private int _TypeId; 

    public GameType Type { get; private set; }

    public int TypeId
    {
        set
        {
            _TypeId = value;
            Type = (GameType)value;
        }
        get => _TypeId;
    }
    [Range(1, 60)]
    public int Timer { get; set; }

    [Range(0, 60)]
    public int TimerIncrement { get; set; }
}