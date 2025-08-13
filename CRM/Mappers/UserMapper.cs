using CRM.Dto.Requests;
using CRM.Dto.Responses;
using CRM.Model;

namespace CRM.Models
{
    public static class UserMapper
    {
        // Map UserRequestDTO + PasswordHash -> User (for creation)
        public static User ToUser(this UserRequestDTO dto, string passwordHash)
        {
            
            return new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = passwordHash,  // Hashed password from service layer
                RoleName = dto.RoleName,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow,  // Auto-set creation time
                LastLogin = null              // Initialize as null
            };
        }

        // Map User -> UserResponseDTO
        public static UserResponseDTO ToUserResponseDTO(this User user)
        {
            return new UserResponseDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                RoleName = user.RoleName,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                LastLogin = user.LastLogin
            };
        }

        // Update existing User from UserRequestDTO
        public static void UpdateFromDTO(this User user, UserRequestDTO dto, string? passwordHash = null)
        {
            user.Username = dto.Username;
            user.Email = dto.Email;
            user.RoleName = dto.RoleName;
            user.IsActive = dto.IsActive;

            // Only update password if provided
            if (passwordHash != null)
            {
                user.PasswordHash = passwordHash;
            }
        }
    }
}