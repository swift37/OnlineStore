using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineStore.Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "E6A2B060-BA58-4364-812A-EBBDB01C45C5",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                new IdentityRole
                {
                    Id = "C67F5372-EE2A-4DDA-B3DC-67E03E94A1C0",
                    Name = "Manager",
                    NormalizedName = "MANAGER"
                },
                new IdentityRole
                {
                    Id = "3DBD3715-E14C-439B-AFA4-77DE58D974B1",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                },
                new IdentityRole
                {
                    Id = "AADA53D4-BE36-446F-98B3-4C675EC376BA",
                    Name = "User",
                    NormalizedName = "USER"
                }
            );
        }
    }
}
