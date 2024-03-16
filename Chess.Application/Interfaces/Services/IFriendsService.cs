using Chess.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Application.Interfaces.Services
{
    public interface IFriendsService
    {
        Task<FriendRequests> SendRequest(Guid senderId, Guid receiverId);
        Task<FriendRequests> UserResponse(Guid userId,Guid friendRequestId, FriendRequestStatus responseStatus);
    }
}
