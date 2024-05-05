using Chess.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Chess.Domain.Entities;

public class Statistic
{
    public Guid Id { get; set; }
    private int _TypeId;
    public GameType Type { get; set; }
    public int TypeId
    {
        set
        {
            _TypeId = value;
            Type = (GameType)value;
        }
        get => _TypeId;
    }
    [Required]
    public int GamesPlayed { get; set; } = 0;
    [Required]
    public int Wins { get; set; } = 0;
    [Required]
    public int Losses { get; set; } = 0;
    [Required]
    public int Draws { get; set; } = 0;
    [Required]
    public int Rating { get; set; } = 1200;
    public int WinPercentage => GamesPlayed > 0 ? (int)((double)Wins / GamesPlayed * 100) : 0;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }
}