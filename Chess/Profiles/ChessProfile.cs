using AutoMapper;
using Chess.DTOs;
using Chess.Models;

namespace Chess.Profiles
{
    public class ChessProfile : Profile
    {
        public ChessProfile()
        {
            CreateMap<RegistrationDto, User>();
            CreateMap<User, PlayerDto>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId.ToString()));
            CreateMap<Stats, StatsDto>();
            CreateMap<GameDto, Game>()
                .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.WhitePlayerId, opt => opt.MapFrom(src => src.WhitePlayer.Id))
                .ForMember(dest => dest.WhitePlayer, opt => opt.Ignore())
                .ForMember(dest => dest.BlackPlayerId, opt => opt.MapFrom(src => src.BlackPlayer.Id))
                .ForMember(dest => dest.BlackPlayer, opt => opt.Ignore())
                .ForMember(dest => dest.WinnerId, opt => opt.MapFrom(src => src.Winner!.Id))
                .ForMember(dest => dest.Winner, opt => opt.Ignore())
                .ForMember(dest => dest.Pgn, opt => opt.MapFrom(src => src.Board.ToPgn()));
            CreateMap<PlayerDto, Stats>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => int.Parse(src.Id)))
                .ForMember(dest => dest.GamesPlayed, opt => opt.MapFrom(src => src.Stats.GamesPlayed))
                .ForMember(dest => dest.Wins, opt => opt.MapFrom(src => src.Stats.Wins))
                .ForMember(dest => dest.Losses, opt => opt.MapFrom(src => src.Stats.Losses))
                .ForMember(dest => dest.Draws, opt => opt.MapFrom(src => src.Stats.Draws))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Stats.Rating));

            CreateMap<ChallengeDto, GameDto>();
        }
    }
}