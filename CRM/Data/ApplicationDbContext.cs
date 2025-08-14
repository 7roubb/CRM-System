using System.Collections.Generic;
using CRM.Model;
using Microsoft.EntityFrameworkCore;

namespace CRM.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_Status> Users_Status { get; set; }
<<<<<<< HEAD
=======
        public DbSet<Notes> Notes { get; set; }
>>>>>>> main
    }
}
