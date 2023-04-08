using System.Collections.Concurrent;
using AutoMapper;
using Chess.DTOs;
using Chess.Models;
using Chess.Repositories;

namespace Chess.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public PlayerService(IUserRepository userRepository, IMapper mapper, ICacheService cacheService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<PlayerDto> CreatePlayerAsync(string userId, GameDto? game = null)
        {
            PlayerDto player;
            if (game != null)
            {
                player = game.WhitePlayer.Id == userId ? game.WhitePlayer : game.BlackPlayer;
            }
            else
            {
                User? user = await _userRepository.GetUserByIdAsync(int.Parse(userId));
                player = _mapper.Map<PlayerDto>(user);
            }

            _cacheService.AddPlayer(player);
            return player;
        }

        public PlayerDto? GetPlayer(string id)
        {
            return _cacheService.GetPlayer(id);
        }

        public PlayerDto? RemovePlayer(string id)
        {
            return _cacheService.RemovePlayer(id);
        }

        public ConcurrentDictionary<string, PlayerDto> GetAllPlayers()
        {
            return _cacheService.GetAllPlayers();
        }

        public IEnumerable<PlayerDto> GetAvailablePlayers()
        {
            return GetAllPlayers().Values.Where(p => p.GameId == Guid.Empty);
        }

        public GameDto? GetChallenge(PlayerDto player)
        {
            if (player.ChallengeId == Guid.Empty)
            {
                return null;
            }
            return _cacheService.GetGame(player.ChallengeId);
        }

        public GameDto? RemoveChallenge(PlayerDto player)
        {
            GameDto? game = _cacheService.RemoveGame(player.ChallengeId);
            player.ChallengeId = Guid.Empty;
            return game;
        }

        public PlayerDto? GetChallenger(PlayerDto player)
        {
            GameDto? game = GetChallenge(player);
            if (game == null)
            {
                return null;
            }

            string challengerId = game.WhitePlayer == player ? game.BlackPlayer.Id : game.WhitePlayer.Id;

            return GetPlayer(challengerId);
        }

    }
}
