using CRM.Data;
using CRM.Dto.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using CRM.Model;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public AccountsController(ApplicationDbContext context)
        {
            this.context = context;

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest dto)
        {
            if (await context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Email is already registered.");

            var role = context.Roles.FirstOrDefault(r => r.RoleName == "User");
            var defaultStatus = await context.Users_Status.FirstOrDefaultAsync(s => s.status == "Active");
            if (defaultStatus == null)
            {
                return BadRequest("Default user status not found.");
            }


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

            context.Users.Add(user);
            await context.SaveChangesAsync();


            return Ok("User registered successfully.");
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LogInRequest request)
        {
            var user = await context.Users
                .Include(u => u.Role)   
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
                return BadRequest(new { message = "Invalid email or password" });

            if (user.PasswordHash != HashPassword(request.Password))
                return BadRequest(new { message = "Invalid email or password" });

            List<Claim> claims = new()
    {
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        new Claim("Username", user.Username),
        new Claim("email", user.Email),
        new Claim("RoleName", user.Role.RoleName)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("wUTTqk2HZStu8PTAlAz5npa93FRDhW39"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = jwt });
        }

    }
}




