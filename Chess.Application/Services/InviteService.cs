using Chess.Application.Interfaces.Caching;
using Chess.Application.Interfaces.Repositories;
using Chess.Application.Interfaces.Services;
using Chess.Domain.Entities;
using System.Text.Json;
using System;
using Chess.Domain.Enums;

namespace Chess.Application.Services
{
    public class InviteService : IInviteService
    {
        private readonly IInviteCache _inviteCache;
        private readonly IPlayerCache _playerCache;
        private readonly IGameCache _gameCache;
        private readonly IGameService _gameService;

        public InviteService(IInviteCache inviteCache, IPlayerCache playerCache, IGameCache gameCache, IGameService gameService)
        {
            _inviteCache = inviteCache;
            _playerCache = playerCache;
            _gameCache = gameCache;
            _gameService = gameService;
        }
        public Invite Reject(Guid toId)
        {
            Invite invite = _inviteCache.Find(toId);
            _inviteCache.Remove(toId);
            return invite;
        }

        public Invite Save(Guid fromId, Invite invite)
        {
            if (fromId == invite.ToId)
            {
                throw new Exception();
            } 
            Game game=_gameCache.Find(invite.ToId);
            Player to = _playerCache.Find(invite.ToId);
            if (game != null || to == null)
            {
                throw new Exception();
            }
            invite.FromId = fromId;
            invite.From = _playerCache.Find(fromId);
            invite.To = to;
            invite.ToColor = invite.FromColor==Color.White?Color.Black:Color.White;
            if (invite.Timer < 3)
            {
                invite.TypeId = (int)GameType.Bullet;
            }
            else if (invite.Timer < 10)
            {
                invite.TypeId = (int)GameType.Blitz;
            }
            else
            {
                invite.TypeId = (int)GameType.Rapid;
            }
            _inviteCache.Add(invite);
            return invite;
        }
        public Game Accept(Guid toId)
        {
            Invite invite = _inviteCache.Find(toId);
            Player inviter = _playerCache.Find(toId);
            if (invite == null || inviter == null || inviter.Game != null)
            {
                throw new Exception();
            }
            Game game = _gameService.Create(invite);
            _inviteCache.Remove(toId);
            return game;
        }
    }
}
