using System.ComponentModel.DataAnnotations;

namespace CRM.Dto.Requests
{
    public class LogInRequest
    {
        [Required]
        [MaxLength(255)]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
