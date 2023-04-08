using System.Collections.Concurrent;
using Chess.DTOs;

namespace Chess.Services
{
    public interface ICacheService
    {
        public bool AddPlayer(PlayerDto player);
        public PlayerDto? GetPlayer(string id);
        public ConcurrentDictionary<string, PlayerDto> GetAllPlayers();
        public PlayerDto? RemovePlayer(string id);
        public bool AddGame(GameDto game);
        public GameDto? GetGame(Guid id);
        public GameDto? GetGameByUserId(string id);
        public GameDto? RemoveGame(Guid id);
        public bool AddChallenge(ChallengeDto challenge);
        public ChallengeDto? GetChallenge(Guid id);
        public ChallengeDto? RemoveChallenge(Guid id);
    }
}
