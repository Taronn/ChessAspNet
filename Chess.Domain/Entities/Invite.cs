using Chess.Domain.Enums;

namespace Chess.Domain.Entities;

public class Invite
{
    public Player From { get; set; }
    public Guid FromId { get; set; }
    public Player To { get; set; }
    public Guid ToId { get; set; }
    public GameConfiguration GameConfiguration { get; set; }
    public DateTime CreatedAt { get; set; }
}