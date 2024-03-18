using Chess.Application.Interfaces.Caching;
using Chess.Application.Interfaces.Repositories;
using Chess.Application.Interfaces.Services;
using Chess.Domain.Entities;
using System;
using AutoMapper;

namespace Chess.Application.Services;

public class PlayerService : IPlayerService
{
    private readonly IUserRepository _userRepository;
    private readonly IPlayerCache _playerCache;

    public PlayerService(IUserRepository userRepository,IPlayerCache playerCache)
    {
        _userRepository = userRepository;
        _playerCache = playerCache;
    }

    public Player Find(Guid id)
    {
        return _playerCache.Find(id);
    }

    public async Task<Player> Join(Guid playerId,Game game=null)
    {
        Player player;
        if (game != null)
        {
            player = game.WhitePlayer.Id == playerId ? game.WhitePlayer : game.BlackPlayer;
        }
        else
        {
            User? user = await _userRepository.FindAsync(playerId);
            player = new Player
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName=user.LastName,
                Username=user.Username,
                Picture=user.Picture,
                Gender=user.Gender,
                Statistics=user.Statistics,
                Country=user.Country         
            };
        }
        _playerCache.Add(player);
        return player;
    }

}