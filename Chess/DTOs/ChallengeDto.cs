namespace Chess.DTOs
{
    public class ChallengeDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public PlayerDto WhitePlayer { get; set; } = null!;
        public PlayerDto BlackPlayer { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ChallengeDto(PlayerDto challenger, PlayerDto opponent, string challengerColor)
        {
            (WhitePlayer, BlackPlayer) = challengerColor == "white" ? (challenger, opponent) : (opponent, challenger);
        }
    }
}