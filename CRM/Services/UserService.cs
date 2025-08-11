using CRM.Config;
using CRM.Exceptions;
using CRM.Models;
using CRM.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CRM.Services.IServices;
using CRM.Dto.Requests;
using CRM.Dto.Responses;

namespace CRM.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(ApplicationDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<UserResponseDTO>> GetAllAsync()
        {
            var users = await _context.Users.AsNoTracking().ToListAsync();
            return users.Select(u => u.ToUserResponseDTO());
        }

        public async Task<UserResponseDTO> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                throw new UserNotFoundException($"User with ID {id} not found.");

            return user.ToUserResponseDTO();
        }

        public async Task<UserResponseDTO> CreateAsync(UserRequestDTO dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                throw new UserAlreadyExistsException($"Email '{dto.Email}' is already registered.");

            if (!IsValidRole(dto.RoleName))
                throw new InvalidRoleException($"Role '{dto.RoleName}' is not valid.");

            string passwordHash = HashPassword(dto.Password);

            var user = dto.ToUser(passwordHash);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user.ToUserResponseDTO();
        }

        public async Task<UserResponseDTO> UpdateAsync(int id, UserRequestDTO dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new UserNotFoundException($"User with ID {id} not found.");

            if (!IsValidRole(dto.RoleName))
                throw new InvalidRoleException($"Role '{dto.RoleName}' is not valid.");

            string? passwordHash = !string.IsNullOrWhiteSpace(dto.Password)
                ? HashPassword(dto.Password)
                : null;

            user.UpdateFromDTO(dto, passwordHash);

            await _context.SaveChangesAsync();

            return user.ToUserResponseDTO();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new UserNotFoundException($"User with ID {id} not found.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        // Simple SHA256 password hashing (for demo — replace with BCrypt/Argon2 in production)
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        // Define allowed roles here
        private bool IsValidRole(string role)
        {
            var allowedRoles = new[] { "Admin", "Sales", "Support", "User" };
            return allowedRoles.Contains(role);
        }
        public async Task<bool>ChangeRole(int UserId, string rolename)
        {
            var user= await _context.Users.FindAsync(UserId);
            if(user is not null)
            {
                var Role = await _context.Roles.FirstOrDefaultAsync(u => u.RoleName == rolename);
               if(Role is not null)
                {
                    user.UserId = Role.RoleId;
                }
               return true;
            }
            else
                return false;
        }
    }
}
