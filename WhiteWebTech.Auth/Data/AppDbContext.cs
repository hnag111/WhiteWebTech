using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WhiteWebTech.Auth.Models;

namespace WhiteWebTech.Auth.Data
{
    public class AppDbContext :IdentityDbContext<ApplicationUsers>
    {
        public AppDbContext( DbContextOptions< AppDbContext> options):base(options)
        {
            
        }

        public DbSet<ApplicationUsers> ApplicationUserss { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
