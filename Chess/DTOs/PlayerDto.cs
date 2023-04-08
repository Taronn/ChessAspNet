namespace Chess.DTOs
{
    public class PlayerDto
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public Guid GameId { get; set; }
        public Guid ChallengeId { get; set; }
        public StatsDto Stats { get; set; } = null!;

    }
}