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

        Task AddItem(
            Guid userId,
            WishlistItem item,
            CancellationToken cancellation = default);

        Task UpdateItem(
            Guid userId,
            WishlistItem item,
            CancellationToken cancellation = default);

        Task RemoveItem(
            Guid userId,
            int itemId,
            CancellationToken cancellation = default);
    }
}
