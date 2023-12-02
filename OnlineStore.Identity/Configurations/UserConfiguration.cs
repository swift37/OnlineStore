using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Identity.Models;

namespace OnlineStore.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            builder.HasData(
                new ApplicationUser
                {
                    Id = "E301AF60-A2CD-457B-A337-3B5BB73208DA",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    PasswordHash = hasher.HashPassword(null, "admin981"),
                    FirstName = "Developer",
                    LastName = "Admin",
                    Email = "admin@onlinestore.com",
                    NormalizedEmail = "ADMIN@ONLINESTORE.COM",
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "0AD36A86-F6CA-4AE5-AC56-C24C2D8DBFCC",
                    UserName = "manager",
                    NormalizedUserName = "MANAGER",
                    PasswordHash = hasher.HashPassword(null, "manager981"),
                    FirstName = "Developer",
                    LastName = "Manager",
                    Email = "manager@onlinestore.com",
                    NormalizedEmail = "MANAGER@ONLINESTORE.COM",
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "A9127F04-292B-4A13-BFD9-F510BC2E2769",
                    UserName = "employee",
                    NormalizedUserName = "EMPLOYEE",
                    PasswordHash = hasher.HashPassword(null, "employee981"),
                    FirstName = "Developer",
                    LastName = "Employee",
                    Email = "employee@onlinestore.com",
                    NormalizedEmail = "EMPLOYEE@ONLINESTORE.COM",
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "F457DC2A-9480-43C3-8136-288098C87117",
                    UserName = "user",
                    NormalizedUserName = "USER",
                    PasswordHash = hasher.HashPassword(null, "user981"),
                    FirstName = "Developer",
                    LastName = "User",
                    Email = "user@onlinestore.com",
                    NormalizedEmail = "USER@ONLINESTORE.COM",
                    EmailConfirmed = true
                }
            );
        }
    }
}
