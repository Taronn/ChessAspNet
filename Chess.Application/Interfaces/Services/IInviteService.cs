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
        Invite Reject(Guid toId);
        Invite Save(Guid fromId, Invite invite);
        Game Accept(Guid toId);

    }
}
