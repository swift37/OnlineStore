using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Identity.Models;

namespace OnlineStore.Identity.Context
{
    public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
