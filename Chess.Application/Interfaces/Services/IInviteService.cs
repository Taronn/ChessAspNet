using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Chess.Application.Services.InviteService;

namespace Chess.Application.Interfaces.Services
{
    public interface IInviteService
    {
        bool IsInGame(Guid fromId);
        void RemoveInvite(Guid toId);
        Invite FindInvite(Player player);

        Invite Save(Guid fromId,Guid toID, Invite invite);
        Player FindInviter(Guid fromId);
        public InviteResult AcceptChallenge(Player player, Guid fromId);

    }
}
