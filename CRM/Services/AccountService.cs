using CRM.Data;
using CRM.Dto.Requests;
using CRM.Dto.Responses;
using CRM.Exceptions;
using CRM.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using CRM.Services.IServices;
using Microsoft.Extensions.Options;

namespace CRM.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtSettings _jwtSettings;

        public AccountService(ApplicationDbContext context, IOptions<JwtSettings> jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                throw new UserAlreadyExistsException("Email is already registered");

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == "User");
            if (role == null)
                throw new ResourceNotFoundException("Default role 'User' not found");

            var defaultStatus = await _context.Users_Status.FirstOrDefaultAsync(s => s.status == "Active");
            if (defaultStatus == null)
                throw new ResourceNotFoundException("Default user status 'Active' not found");

            var passwordHash = HashPassword(dto.Password);

            var user = new User
            {
                Username = dto.UserName,
                Email = dto.Email,
                PasswordHash = passwordHash,
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                User_Status_ID = defaultStatus.Id
            };

            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new DatabaseOperationException("Failed to create user");
            }

            return new RegisterResponse
            {
                UserId = user.UserId,
                Message = "User registered successfully"
            };
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
                throw new AuthenticationFailedException("Invalid email or password");

            if (user.PasswordHash != HashPassword(request.Password))
                throw new AuthenticationFailedException("Invalid email or password");

            try
            {
                var token = GenerateJwtToken(user);
                return new LoginResponse { Token = token };
            }
            catch (Exception ex)
            {
                throw new ConfigurationException("JWT token generation failed");
            }
        }

        private string HashPassword(string password)
        {
            try
            {
                using var sha = SHA256.Create();
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
            catch (Exception ex)
            {
                throw new ConfigurationException("Password hashing failed");
            }
        }

        private string GenerateJwtToken(User user)
        {
            if (string.IsNullOrEmpty(_jwtSettings.SecretKey))
                throw new ConfigurationException("JWT secret key is not configured");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim("Username", user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.RoleName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}