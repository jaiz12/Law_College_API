using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DTO.Models.Auth;

namespace Common.DataContext
{
    // Added by Gautam
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly DbContextOptions _options;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public new DbSet<ApplicationUser> Users { get; set; }


    }
}
