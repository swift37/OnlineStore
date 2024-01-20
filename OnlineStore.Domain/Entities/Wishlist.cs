using OnlineStore.Domain.Base;

namespace OnlineStore.Domain.Entities
{
    public class Wishlist : Entity
    {
        public Guid UserId { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public DateTime LastChangeDate { get; set; } = DateTime.Now;

        public ICollection<WishlistItem> Items { get; set; } = new HashSet<WishlistItem>();

        public int ProductsQuantity => Items.Count;
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
