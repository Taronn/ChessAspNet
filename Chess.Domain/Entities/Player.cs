using Chess.Domain.Enums;

namespace Chess.Domain.Entities;

public class Player
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public string Country { get; set; }
    public string Picture { get; set; }
    public List<Statistic> Statistics { get; set; }
    public List<Invite> Invites { get; set; }
    public Game Game { get; set; }
}