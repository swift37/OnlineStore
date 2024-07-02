using Microsoft.EntityFrameworkCore;
using OnlineStore.Identity.Context;

namespace OnlineStore.Identity
{
    public static class IdentityDbInitializer
    {
        public static void Initialize(ApplicationIdentityDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
