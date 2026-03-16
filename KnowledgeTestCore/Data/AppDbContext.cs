using KnowledgeTestCore.Models;
using Microsoft.EntityFrameworkCore;

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
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Username = "admin",
                // ✅ NEW CORRECT HASH!
                PasswordHash = "$2a$11$C402WNPB89YHDRezXfTss.c46MsCTlBnnlZk9VFhxFS.gd.ZViFk6",
                Role = "Admin"
            });
        }
    }
}