using JopSy.Models;
using Microsoft.EntityFrameworkCore;

namespace JopSy.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Address>().HasData(
                new Address { Id = 1, City = "New York", Area = "Manhattan", Street = "Fifth Avenue" },
                new Address { Id = 2, City = "London", Area = "Westminster", Street = "Baker Street" },
                new Address { Id = 3, City = "Tokyo", Area = "Shibuya", Street = "Dogenzaka" }
            );
        }
    }
}