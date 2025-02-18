using System.ComponentModel.DataAnnotations;

namespace Talabat.Api.Dtos
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", 
          ErrorMessage = "Password must have at least one uppercase letter, one lowercase letter, one number, and be between 8 and 15 characters.")]
        public string Password { get; set; }

    }
}
