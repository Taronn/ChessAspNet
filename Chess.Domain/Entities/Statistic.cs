using Chess.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Chess.Domain.Entities;

public class Statistic
{
    public Guid Id { get; set; }
    public GameType Type { get; set; }
    public int TypeId
    {
        set => Type = (GameType)value;
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