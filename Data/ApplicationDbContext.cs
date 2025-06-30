using JopSy.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JopSy.ViewModel;

namespace JopSy.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CreateJobViewModel> CreateJobViewModel { get; set; } = default!;
        public DbSet<EditJobViewModel> EditJobViewModel { get; set; } = default!;
      

       
    }
}