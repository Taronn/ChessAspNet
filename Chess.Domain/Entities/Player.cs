using Chess.Domain.Enums;

namespace Chess.Domain.Entities;

public class Player
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public string Country { get; set; }
    public string Picture { get; set; }
    public List<Statistic> Statistics { get; set; }
    public Invite Invite { get; set; }
    public Game Game { get; set; }
}