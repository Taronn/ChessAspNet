using Chess.Domain.Enums;

namespace Chess.Domain.Entities;

public class Invite:GameConfiguration
{
    public Player From { get; set; }
    public Guid FromId { get; set; }
    public Player To { get; set; }
    public Guid ToId { get; set; }
    public string Message { get; set; }
    public Color FromColor { get; set; }
    public Color ToColor { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

}