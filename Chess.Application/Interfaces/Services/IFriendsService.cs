﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Application.Interfaces.Services
{
    public interface IFriendsService
    {
        Task<bool> SendFriendRequest(int senderId, int recieverId);
    }
}
