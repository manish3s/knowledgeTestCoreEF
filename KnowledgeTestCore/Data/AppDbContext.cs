using KnowledgeTestCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace KnowledgeTestCore.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Default admin user when DB is created
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Username = "admin",
                PasswordHash = "$2a$11$N5E1n2KMSEh4gKHLkPEsNOlQGYBRBVWDnPMTFMoM1/zBgbKe7rNYi",
                Role = "Admin"
            });
        }
    }

}
