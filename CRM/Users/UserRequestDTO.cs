using System.ComponentModel.DataAnnotations;

namespace CRM.Models
{
    public class UserRequestDTO
    {
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(100, ErrorMessage = "Username cannot exceed 100 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(255, ErrorMessage = "Email cannot exceed 255 characters")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }  // Plain-text password (will be hashed)

        [MaxLength(50, ErrorMessage = "Role cannot exceed 50 characters")]
        public string Role { get; set; } = "User";  // Default role

        public bool IsActive { get; set; } = true;
    }
}