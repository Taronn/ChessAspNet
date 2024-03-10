using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Domain.Entities
{
    public class Friends
    {
        public Guid Id { get; set; }
        public Player User1 { get; set; }
        public Guid UserId1 { get; set; }
        public Player User2 { get; set; }
        public Guid UserId2 { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
