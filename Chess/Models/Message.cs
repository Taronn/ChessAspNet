using System.ComponentModel.DataAnnotations;

namespace Chess.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Content { get; set; } = null!;

        [Required]
        public DateTime TimeSent { get; set; } = DateTime.Now;

        public int? UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid? GameId { get; set; }

        public Game? Game { get; set; }
    }
}