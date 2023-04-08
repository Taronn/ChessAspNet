using AutoMapper;
using Chess.DTOs;
using Chess.Repositories;

namespace Chess.Services
{
    public class ChallengeService : IChallengeService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly IGameService _gameService;

        public ChallengeService(IUserRepository userRepository, IMapper mapper, ICacheService cacheService, IGameService gameService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _cacheService = cacheService;
            _gameService = gameService;
        }

        public ChallengeDto? CreateChallenge(PlayerDto challenger, PlayerDto opponent, string challengerColor)
        {
            if (opponent.ChallengeId != Guid.Empty || opponent.GameId != Guid.Empty)
            {
                return null;
            }
            ChallengeDto challenge = new ChallengeDto(challenger, opponent, challengerColor);
            opponent.ChallengeId = challenge.Id;
            _cacheService.AddChallenge(challenge);
            return challenge;
        }


        public ChallengeDto? GetChallenge(PlayerDto player)
        {
            if (player.ChallengeId == Guid.Empty)
            {
                return null;
            }
            return _cacheService.GetChallenge(player.ChallengeId);
        }

        public ChallengeDto? RemoveChallenge(PlayerDto player)
        {
            ChallengeDto? challenge = _cacheService.RemoveChallenge(player.ChallengeId);
            player.ChallengeId = Guid.Empty;
            return challenge;
        }

        public PlayerDto? GetChallenger(PlayerDto player)
        {
            ChallengeDto? challenge = GetChallenge(player);
            if (challenge == null)
            {
                return null;
            }

            string challengerId = challenge.WhitePlayer == player ? challenge.BlackPlayer.Id : challenge.WhitePlayer.Id;

            return _cacheService.GetPlayer(challengerId);
        }

        public ChallengeResult AcceptChallenge(PlayerDto player)
        {
            ChallengeDto? challenge = GetChallenge(player);
            PlayerDto? challenger = GetChallenger(player);
            if (challenge == null)
            {
                return ChallengeResult.NoChallengeExists;
            }
            if (challenger == null)
            {
                return ChallengeResult.ChallengerOffline;
            }
            if (challenger.GameId != Guid.Empty)
            {
                return ChallengeResult.ChallengerAlreadyInGame;
            }

            player.ChallengeId = Guid.Empty;
            _gameService.CreateGame(challenge);
            return ChallengeResult.Success;
        }

    }

    public enum ChallengeResult
    {
        Success,
        NoChallengeExists,
        ChallengerOffline,
        ChallengerAlreadyInGame,
    }
    // public class ChallengeResult
    // {
    //     public bool Success { get; set; }
    //     public string Message { get; set; }
    //     public ChallengeDto? Challenge { get; set; }
    //     public ChallengeResult(bool success, string message, ChallengeDto? challenge = null)
    //     {
    //         Success = success;
    //         Message = message;
    //         Challenge = challenge;
    //     }
    // }
}
