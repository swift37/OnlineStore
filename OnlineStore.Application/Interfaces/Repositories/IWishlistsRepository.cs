using OnlineStore.Domain;

namespace OnlineStore.Application.Interfaces.Repositories
{
    public interface IWishlistsRepository : IRepository<Wishlist>
    {
        Task<IEnumerable<Wishlist>> GetUserWishlistAsync(Guid userId);
    }
}
