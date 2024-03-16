using Chess.Domain.Enums;
using System.Threading.Tasks;

namespace Chess.Application.Interfaces.Repositories
{
    public interface IFriendsRepository
    {
        Task<FriendRequests> SaveRequest(Guid senderId, Guid receiverId);
        Task<bool> IsRequestSent(Guid senderId, Guid receiverId);
        Task<bool> IsFriends(Guid userId1, Guid userId2);
        Task<FriendRequests> GetRequest(Guid id);
        Task<FriendRequests> UpdateRequest(Guid requestId, FriendRequestStatus status);

    }
}
