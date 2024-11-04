using System.ComponentModel.DataAnnotations;

namespace EventManagement.Common.Dto
{
    public class UserRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
