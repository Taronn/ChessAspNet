using Chess.Domain.Enums;

namespace Chess.Domain.Entities;

public class Setting
{
    public string Id { get; set; }
    public Theme Theme { get; set; }
    public bool DarkMode { get; set; }
    public Language Language { get; set; }
    public string ColorTheme { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}