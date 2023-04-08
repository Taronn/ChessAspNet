using System.ComponentModel.DataAnnotations;

namespace Chess.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(15)]
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(60)]
        public string Password
        {
            get => _password;
            set => _password = BCrypt.Net.BCrypt.HashPassword(value, BCrypt.Net.BCrypt.GenerateSalt());
        }
        private string _password = null!;

        [MaxLength(30)]
        public string? Country { get; set; }

        public DateTime CreatedDate { get; } = DateTime.Now;

        public Stats? Stats { get; set; }


        public bool ValidatePassword(string? password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }


    }
}
