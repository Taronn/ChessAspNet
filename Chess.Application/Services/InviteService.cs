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

        public InviteService(IInviteCache inviteCache, IPlayerCache playerCache)
        {
            _inviteCache = inviteCache;
            _playerCache = playerCache;
        }

        public bool IsInGame(Guid fromId)
        {
            return _playerCache.Find(fromId) == null;
        }
        public Invite Find(Player player)
        {
            if (player.Invite.FromId == Guid.Empty)
            {
                return null;
            }
            return _inviteCache.Find(player.Invite.FromId);
        }

    /*    public Invite Remove(Player player)
        {
            Invite invite = _inviteCache.Remove(player.Invite.FromId);
            player.ChallengeId = Guid.Empty;
            return challenge;
        }*/

        public Invite Save(Guid fromId,Guid toId, Invite invite)
        {

            switch (invite.FromColor)
            {
                case Color.White:
                    invite.ToColor = Color.Black;
                    break;
                case Color.Black:
                    invite.ToColor = Color.White;
                    break;
                default:
                    throw new ArgumentException("Wrong color");
            }

            if (fromId == toId)
            {
                throw new ArgumentException("FromID=inviteID");

            }
            try
            {
                invite.FromId = fromId;
                invite.ToId = toId;
                invite.From = _playerCache.Find(fromId);
                invite.To = _playerCache.Find(toId);
                _inviteCache.Add(invite);
                return invite;
            }
            catch
            {
                throw new ArgumentException("Cannot create new Invite");
            }

        }
    }
}
