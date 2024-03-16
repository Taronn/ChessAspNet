using Chess.Application.Interfaces.Repositories;
using Chess.Application.Interfaces.Services;
using Chess.Domain.Enums;

namespace Chess.Application.Services
{
    public class FriendsService : IFriendsService
    {
        private readonly IFriendsRepository _friendsRepository;
        private readonly IUserRepository _userRepository;
        public FriendsService(IFriendsRepository friendsRepository, IUserRepository userRepository)
        {
            this._friendsRepository = friendsRepository;
            this._userRepository = userRepository;
        }
        public async Task<FriendRequests> SendRequest(Guid senderId, Guid receiverId)
        {
            if(senderId == receiverId)
            {
                throw new Exception("ERROR");
            }

            var user = await _userRepository.FindAsync(receiverId);
            if (user == null)
            {
                throw new Exception("User is not exists");
            }

            var isFriends = await _friendsRepository.IsFriends(senderId, receiverId);
            if(isFriends)
            {
                throw new Exception("You are already friends");
            }

            var isRequestSent = await _friendsRepository.IsRequestSent(senderId, receiverId);
            if (isRequestSent)
            {
                throw new Exception("It is not possible to send a friend request more than once.");
            }

            var friendRequest = await _friendsRepository.SaveRequest(senderId, receiverId);
            return friendRequest;
        }

        public async Task<FriendRequests> UserResponse(Guid userId, Guid friendRequestId, FriendRequestStatus responseStatus)
        {
            var request = await _friendsRepository.GetRequest(friendRequestId);
            if (request == null)
            {
                throw new Exception("Friend request is missing.");
            }
            if(request.ReceiverId != userId)
            {
                throw new Exception("You cannot answer this request"); 
            }

            var friendRequest = await _friendsRepository.UpdateRequest(request.Id, responseStatus);
            return friendRequest;
        }
    }
}
