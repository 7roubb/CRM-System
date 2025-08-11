using Microsoft.EntityFrameworkCore;
using CRM.Model;
using CRM.Data;
using System.Text;
using System.Security.Cryptography;
using CRM.Uitlity.DBInitlizer;

namespace CRM.Utility.DBInitializer
{
    public class DBInitlizerService : IDBInitlizer
    {
        private readonly ApplicationDbContext context;

        public DBInitlizerService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task initlizerAsync()
        {
            try
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Role { RoleName = "Admin" },
                    new Role { RoleName = "Sales" },
                    new Role { RoleName = "Support" },
                    new Role { RoleName = "User" }
                );
                await context.SaveChangesAsync();
            }

            if (!context.Users_Status.Any())
            {
                context.Users_Status.AddRange(
                    new User_Status {  status = "Active" },
                    new User_Status {  status = "Inactive" }
                );
                await context.SaveChangesAsync();
            }


            var role = context.Roles.FirstOrDefault(r => r.RoleName == "Admin");
            if (role == null)
            {
                Console.WriteLine("لم يتم العثور على دور Admin.");
                return;
            }


            var userStatus = context.Users_Status.FirstOrDefault(s => s.status == "Active");
            if (userStatus == null)
            {
                Console.WriteLine("لم يتم العثور على حالة المستخدم Active.");
                return;
            }

            if (!context.Users.Any(u => u.Email == "binih96493@cotasen.com"))
            {
                var user = new User
                {
                    Username = "Admin",
                    Email = "binih96493@cotasen.com",
                    PasswordHash = HashPassword("Admin@1boss"),
                    RoleId = role.RoleId,
                    RoleName = role.RoleName,
                    User_Status_ID = userStatus.Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
