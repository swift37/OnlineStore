using OnlineStore.Domain.Base;

namespace OnlineStore.Domain
{
    public class Cart : Entity
    {
        public ICollection<CartItem> Items { get; set; } = new HashSet<CartItem>();

        public int ItemsQuantity => Items.Sum(i => i.Quantity);
    }

    public class CartItem : Entity
    {
        public Product? Product { get; set; }

        public int Quantity { get; set; }
    }
}
