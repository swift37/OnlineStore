using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.DAL.Context;
using OnlineStore.Domain;

namespace OnlineStore.DAL.Repositories
{
    public class WishlistsRepository : Repository<Wishlist>, IWishlistsRepository
    {
        public WishlistsRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Wishlist>> GetUserWishlistAsync(Guid userId) => await DbSet
            .Where(o => o.UserId == userId)
            .ToArrayAsync();
    }
}
