using Chess.Domain.Enums;

namespace Chess.Domain.Entities;

public class Setting
{
    public Guid Id { get; set; }
    public Theme Theme { get; set; }
    public int ThemeId
    {
        set => Theme = (Theme)value;
    }
    public bool DarkMode { get; set; }
    public Language Language { get; set; }
    public int LanguageId
    {
        set => Language = (Language)value;
    }
    public string ColorTheme { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}