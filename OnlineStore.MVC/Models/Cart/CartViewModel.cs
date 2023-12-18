using OnlineStore.MVC.Models.Product;

namespace OnlineStore.MVC.Models.Cart
{
    public class CartViewModel
    {
        public ICollection<CartItemViewModel> Items { get; set; } = new HashSet<CartItemViewModel>();

        public int ItemsQuantity => Items.Sum(i => i.Quantity);

        public decimal Subtotal => Items.Sum(i => i.Subtotal);

        public decimal Discount => Items.Sum(i => i.Discount);

        public bool IsEmpty => Items.Count < 1;
    }

    public class CartItemViewModel
    {
        public int ProductId { get; set; }

        public ProductViewModel? Product { get; set; }

        public int Quantity { get; set; }

        public decimal Subtotal => 
            Product is { } ? Product.PriceAfterDiscount * Quantity : default;

        public decimal Discount =>
            Product is { } ? Product.Discount * Quantity : default;
    }
}
