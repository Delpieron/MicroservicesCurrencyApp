using System.ComponentModel.DataAnnotations;

namespace RestApiMicroService.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Username { get; set; }

        [Required]
        [MaxLength(200)]
        public required string PasswordHash { get; set; }

        [Required]
        [MaxLength(200)]
        public required string PasswordSalt { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Email { get; set; }

        public ICollection<Currency> CryptoDatas { get; set; } = [];

    }
}
