using OnlineStore.Domain.Base;

namespace OnlineStore.Domain.Entities
{
    public class Wishlist : Entity
    {
        public Guid UserId { get; set; }

        public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset LastChangeDate { get; set; } = DateTimeOffset.Now;

        public ICollection<WishlistItem> Items { get; set; } = new HashSet<WishlistItem>();
    }

    public class WishlistItem : Entity
    {
        public int WishlistId { get; set; }

        public Wishlist? Wishlist { get; set; }

        public int ProductId { get; set; }

        public Product? Product { get; set; }

        public int Quantity { get; set; }
    }
}
