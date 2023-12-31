using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces.Repositories
{
    public interface IWishlistsRepository : IRepository<Wishlist>
    {
        Task<Wishlist> GetUserWishlistAsync(
            Guid userId, 
            CancellationToken cancellation = default);

        Task<Wishlist> GetOrCreateAsync(
            Guid userId,
            CancellationToken cancellation = default);
    }
}
