using Chess.Domain.Enums;

namespace Chess.Domain.Entities;

public class Invite
{
    public int Id { get; set; }
    public Player From { get; set; }
    public int FromId { get; set; }
    public Player To { get; set; }
    public int ToId { get; set; }
    public GameConfiguration GameConfiguration { get; set; }
    public DateTime CreatedAt { get; set; }
}