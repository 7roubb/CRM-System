using System.Collections.Generic;
using CRM.Models; 
using Microsoft.EntityFrameworkCore;

namespace CRM.Data
{
    public class CRMDbContext : DbContext
    {
        public CRMDbContext(DbContextOptions<CRMDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
