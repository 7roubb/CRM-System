using System.ComponentModel.DataAnnotations;

namespace CRM.Dto.Requests
{
    public class LoginRequest
    {
        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
