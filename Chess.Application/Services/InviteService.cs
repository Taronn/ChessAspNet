using Chess.Application.Interfaces.Caching;
using Chess.Application.Interfaces.Repositories;
using Chess.Application.Interfaces.Services;
using Chess.Domain.Entities;
using System;

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

        public bool IsInGame(int fromId)
        {
            return _playerCache.Find(fromId) == null;
        }

        public void Create(int fromId, dynamic invite)
        {
            if (fromId == invite.ToId)
            {
                throw new ArgumentException("FromID=inviteID");
                
            }
            try
            {
                Invite newInvite = new Invite();
                newInvite.FromId = fromId;
                newInvite.ToId = invite.ToId;
                newInvite.CreatedAt = DateTime.Now;
                newInvite.From = _playerCache.Find(fromId);
                newInvite.To = _playerCache.Find(invite.ToId);

            }
            catch
            {
                throw new ArgumentException("Cannot create new Invite");
            }

        }
    }
}
