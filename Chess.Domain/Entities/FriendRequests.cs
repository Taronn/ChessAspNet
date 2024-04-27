using Chess.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Domain.Entities
{
    public class FriendRequests
    {
        public Guid Id { get; set; }
        public Player Sender { get; set; }
        public Guid SenderId { get; set; }
        public Player Receiver { get; set; }
        public Guid ReceiverId { get; set; }
        public FriendRequestStatus Status { get; set; }
        public int StatusId
        {
            set => Status = (FriendRequestStatus)value;
        }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}