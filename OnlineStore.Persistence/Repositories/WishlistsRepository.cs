using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Exeptions;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.Repositories
{
    public class WishlistsRepository : Repository<Wishlist>, IWishlistsRepository
    {
        public WishlistsRepository(IApplicationDbContext context) : base(context) { }

        public async Task<Wishlist> GetUserWishlistAsync(
            Guid userId, 
            CancellationToken cancellation = default) => await Entities
            .FirstOrDefaultAsync(o => o.UserId == userId, cancellation)
            .ConfigureAwait(false) 
            ?? throw new NotFoundException(nameof(Wishlist), string.Empty);

        public async Task<Wishlist> GetOrCreateAsync(
            Guid userId,
            CancellationToken cancellation = default)
        {
            var wishlist = await Entities
                .Include(w => w.Items)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(w => w.UserId == userId, cancellation)
                .ConfigureAwait(false);

            if (wishlist is null)
            {
                wishlist = new Wishlist() { UserId = userId };

                await DbSet.AddAsync(wishlist, cancellation);
                if (AutoSaveChanges)
                    await _context.SaveChangesAsync(cancellation).ConfigureAwait(false);
            }

            return wishlist;
        }

        public async Task AddItem(
            Guid userId, 
            WishlistItem item, 
            CancellationToken cancellation = default)
        {
            var wishlist = await GetOrCreateAsync(userId, cancellation).ConfigureAwait(false);
            if (wishlist is null)
                throw new Exception("An error occurred when obtaining the wishlist.");
            if (wishlist.Items.Any(item => item.ProductId == item.ProductId))
                throw new Exception("This product is already in your wishlist.");

            wishlist.LastChangeDate = DateTime.UtcNow;
            wishlist.Items.Add(item);

            DbSet.Update(wishlist);
            if (AutoSaveChanges)
                await _context.SaveChangesAsync(cancellation).ConfigureAwait(false);
        }

        public async Task UpdateItem(
            Guid userId,
            WishlistItem item,
            CancellationToken cancellation = default)
        {
            if (Entities
                .Where(w => w.UserId == userId)
                .SelectMany(w => w.Items)
                .SingleOrDefault(i => i.Id == item.Id)
                is WishlistItem updatedItem)
            {
                updatedItem.Quantity = item.Quantity;
            }
            else
                throw new NotFoundException("Your wishlist does not contain such a product.", nameof(Wishlist));

            if (AutoSaveChanges)
                await _context.SaveChangesAsync(cancellation).ConfigureAwait(false);
        }

        public async Task RemoveItem(
            Guid userId,
            int itemId,
            CancellationToken cancellation = default)
        {
            var wishlist = await GetUserWishlistAsync(userId, cancellation).ConfigureAwait(false);
            if (wishlist is null)
                throw new NotFoundException("Wishlist doesn't exist.", nameof(Wishlist));

            if (wishlist.Items.SingleOrDefault(item => item.Id == itemId) is WishlistItem item)
            {
                wishlist.Items.Remove(item);
                wishlist.LastChangeDate = DateTime.UtcNow;
            }
            else
                throw new Exception("Your wishlist does not contain such a product.");       

            if (AutoSaveChanges)
                await _context.SaveChangesAsync(cancellation).ConfigureAwait(false);
        }

        public async Task RemoveItems(
            Guid userId,
            ICollection<int> itemIds,
            CancellationToken cancellation = default)
        {
            var wishlist = await GetUserWishlistAsync(userId, cancellation).ConfigureAwait(false);
            if (wishlist is null)
                throw new NotFoundException("Wishlist doesn't exist.", nameof(Wishlist));

            foreach (var itemId in itemIds)
                if (wishlist.Items.SingleOrDefault(item => item.Id == itemId) is WishlistItem item)
                    wishlist.Items.Remove(item);
                else
                    throw new Exception("Your wishlist does not contain one of the products.");

            wishlist.LastChangeDate = DateTime.UtcNow;

            if (AutoSaveChanges)
                await _context.SaveChangesAsync(cancellation).ConfigureAwait(false);
        }

        public async Task<bool> CheckProductPresence(
            Guid userId,
            int productId,
            CancellationToken cancellation = default) =>
            await Entities.AnyAsync(w => w.UserId == userId && w.Items
                .Any(i => i.ProductId == productId), cancellation).
            ConfigureAwait(false);

        public async Task<int> GetItemId(
            Guid userId,
            int productId,
            CancellationToken cancellation = default) =>
            await Entities.Where(w => w.UserId == userId)
            .SelectMany(w => w.Items)
            .Where(i => i.ProductId == productId)
            .Select(i => i.Id)
            .FirstOrDefaultAsync(cancellation)
            .ConfigureAwait(false);
    }
}
