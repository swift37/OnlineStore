using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.Context;

namespace OnlineStore.DAL
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.Migrate();
        }
    }
}
