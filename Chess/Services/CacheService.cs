using Chess.DTOs;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace Chess.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private const string PLAYERS_KEY = "Players";
        private const string GAMES_KEY = "Games";
        private const string CHALLENGES_KEY = "Challenges";
        public ConcurrentDictionary<string, PlayerDto> Players => _cache.Get<ConcurrentDictionary<string, PlayerDto>>(PLAYERS_KEY)!;
        public ConcurrentDictionary<Guid, GameDto> Games => _cache.Get<ConcurrentDictionary<Guid, GameDto>>(GAMES_KEY)!;
        public ConcurrentDictionary<Guid, ChallengeDto> Challenges => _cache.Get<ConcurrentDictionary<Guid, ChallengeDto>>(CHALLENGES_KEY)!;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
            _cache.Set<ConcurrentDictionary<string, PlayerDto>>(PLAYERS_KEY, new ConcurrentDictionary<string, PlayerDto>());
            _cache.Set<ConcurrentDictionary<Guid, GameDto>>(GAMES_KEY, new ConcurrentDictionary<Guid, GameDto>());
            _cache.Set<ConcurrentDictionary<Guid, ChallengeDto>>(CHALLENGES_KEY, new ConcurrentDictionary<Guid, ChallengeDto>());
        }

        public bool AddPlayer(PlayerDto player)
        {
            return Players.TryAdd(player.Id, player);
        }

        public PlayerDto? GetPlayer(string id)
        {
            Players.TryGetValue(id, out PlayerDto? player);
            return player;
        }

        public ConcurrentDictionary<string, PlayerDto> GetAllPlayers()
        {
            return Players;
        }

        public PlayerDto? RemovePlayer(string id)
        {
            Players.TryRemove(id, out PlayerDto? player);
            return player;
        }

        public bool AddGame(GameDto game)
        {
            return Games.TryAdd(game.Id, game);
        }

        public GameDto? GetGame(Guid id)
        {
            Games.TryGetValue(id, out GameDto? game);
            return game;
        }

        public GameDto? GetGameByUserId(string id)
        {
            return Games.Values.Where(g => (g.WhitePlayer.Id == id || g.BlackPlayer.Id == id)).FirstOrDefault();
        }

        public GameDto? RemoveGame(Guid id)
        {
            Games.TryRemove(id, out GameDto? game);
            return game;
        }

        public bool AddChallenge(ChallengeDto challenge)
        {
            return Challenges.TryAdd(challenge.Id, challenge);
        }

        public ChallengeDto? GetChallenge(Guid id)
        {
            Challenges.TryGetValue(id, out ChallengeDto? challenge);
            return challenge;
        }


        public ChallengeDto? RemoveChallenge(Guid id)
        {
            Challenges.TryRemove(id, out ChallengeDto? challenge);
            return challenge;
        }

    }
}
