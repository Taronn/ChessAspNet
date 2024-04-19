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
        public Invite FindInvite(Player player)
        {
            if (player.Invite.FromId == Guid.Empty)
            {
                return null;
            }
            return _inviteCache.Find(player.Invite.FromId);
        }

        public void RemoveInvite(Guid toId)
        {
            _inviteCache.Remove(toId);
        }

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
        public Player FindInviter(Guid fromId)
        {
            
            return _playerCache.Find(fromId);
        }
        public InviteResult AcceptChallenge(Player player,Guid fromId)
        {
            Invite invite = FindInvite(player);
            Player inviter = FindInviter(fromId);
            if (invite == null)
            {
                return InviteResult.NoChallengeExists;
            }
            if (inviter == null)
            {
                return InviteResult.ChallengerOffline;
            }
            /*if (inviter.GameId != Guid.Empty)
            {
                return ChallengeResult.ChallengerAlreadyInGame;
            }*/

           /* player.ChallengeId = Guid.Empty;
            _gameService.CreateGame(challenge);*/
            return InviteResult.Success;
        }

        public Invite Find(Player player)
        {
            throw new NotImplementedException();
        }

        public enum InviteResult
        {
            Success,
            NoChallengeExists,
            ChallengerOffline,
            ChallengerAlreadyInGame,
        }
    }
}
