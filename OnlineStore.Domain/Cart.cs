using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain
{
    public class Cart
    {
        public ICollection<CartItem> Items { get; set; } = new HashSet<CartItem>();

        public int ItemsQuantity => Items.Sum(i => i.Quantity);
    }

    public class CartItem
    {
        public Product? Product { get; set; }

        public int Quantity { get; set; }
    }
}
