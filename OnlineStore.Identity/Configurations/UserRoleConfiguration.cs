using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineStore.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "E6A2B060-BA58-4364-812A-EBBDB01C45C5",
                    UserId = "E301AF60-A2CD-457B-A337-3B5BB73208DA"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "C67F5372-EE2A-4DDA-B3DC-67E03E94A1C0",
                    UserId = "0AD36A86-F6CA-4AE5-AC56-C24C2D8DBFCC"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "3DBD3715-E14C-439B-AFA4-77DE58D974B1",
                    UserId = "A9127F04-292B-4A13-BFD9-F510BC2E2769"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "AADA53D4-BE36-446F-98B3-4C675EC376BA",
                    UserId = "F457DC2A-9480-43C3-8136-288098C87117"
                }
            );
        }
    }
}
