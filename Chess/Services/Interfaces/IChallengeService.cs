using Chess.DTOs;

namespace Chess.Services
{
    public interface IChallengeService
    {
        public ChallengeDto? CreateChallenge(PlayerDto challenger, PlayerDto opponent, string opponentColor);
        public ChallengeDto? GetChallenge(PlayerDto player);
        public ChallengeDto? RemoveChallenge(PlayerDto player);
        public PlayerDto? GetChallenger(PlayerDto player);
        public ChallengeResult AcceptChallenge(PlayerDto player);

    }
}
