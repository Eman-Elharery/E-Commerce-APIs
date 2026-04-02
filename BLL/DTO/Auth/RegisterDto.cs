using System.ComponentModel.DataAnnotations;

namespace CompanySystem.BLL
{
    public class RegisterDto
    {
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; } = "User";
    }
}
