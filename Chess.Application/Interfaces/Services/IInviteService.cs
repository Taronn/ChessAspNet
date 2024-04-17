using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Application.Interfaces.Services
{
    public interface IInviteService
    {
        bool IsInGame(Guid fromId);
        Invite Find(Player player);
/*        Invite Remove(Player player);
*/        Invite Save(Guid fromId,Guid toID, Invite invite);
    }
}
