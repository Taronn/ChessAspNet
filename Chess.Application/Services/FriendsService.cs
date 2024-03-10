using Chess.Application.Interfaces.Repositories;
using Chess.Application.Interfaces.Services;

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
        public async Task<FriendRequests> SendRequest(int senderId, int recieverId)
        {
            if(senderId == recieverId)
            {
                throw new Exception("ERROR");
            }

            var user = await _userRepository.FindAsync(recieverId);
            if (user == null)
            {
                throw new Exception("User is not exists");
            }

            var isFriends = await _friendsRepository.IsFriends(senderId, recieverId);
            if(isFriends)
            {
                throw new Exception("You are already friends");
            }

            var isRequestSent = await _friendsRepository.IsRequestSent(senderId, recieverId);
            if (isRequestSent)
            {
                throw new Exception("It is not possible to send a friend request more than once.");
            }

            var friendRequest = await _friendsRepository.SaveRequest(senderId, recieverId);
            return friendRequest;
        }
    }
}
