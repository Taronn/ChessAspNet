using System.Collections.Concurrent;
using Chess.DTOs;

namespace Chess.Services
{
    public interface IPlayerService
    {
        public Task<PlayerDto> CreatePlayerAsync(string userId, GameDto? game = null);
        public PlayerDto? GetPlayer(string id);
        public ConcurrentDictionary<string, PlayerDto> GetAllPlayers();
        public IEnumerable<PlayerDto> GetAvailablePlayers();
        public PlayerDto? RemovePlayer(string id);
        public GameDto? GetChallenge(PlayerDto player);
        public GameDto? RemoveChallenge(PlayerDto player);
        public PlayerDto? GetChallenger(PlayerDto player);

    }
}
