using System.ComponentModel.DataAnnotations;

namespace EventManagement.Frontend.Models
{
    public class UserLoginDto
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
