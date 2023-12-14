using OnlineStore.MVC.Models.Product;

namespace OnlineStore.MVC.Models.Cart
{
    public class CartViewModel
    {
        public ICollection<CartItemViewModel> Items { get; set; } = new HashSet<CartItemViewModel>();

        public int ItemsQuantity => Items.Sum(i => i.Quantity);
    }

    public class CartItemViewModel
    {
        public int ProductId { get; set; }

        public ProductViewModel? Product { get; set; }

        public int Quantity { get; set; }
    }
}
