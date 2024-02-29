using Chess.Domain.Enums;

namespace Chess.Domain.Entities;

public class User
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public bool IsVerified { get; set; }
    public string VerificationToken { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Picture { get; set; }
    public string Country { get; set; }
    public Gender Gender { get; set; }
    public string Password { get; set; }
    public string GoogleId { get; set; }
    public string FacebookId { get; set; }
    public DateTime LastLogin { get; set; }
    public DateTime RevokedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Setting Settings { get; set; }
    public List<Statistic> Statistics { get; set; }
}