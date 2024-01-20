using OnlineStore.MVC.Models.Product;

namespace OnlineStore.MVC.Models.Wishlist
{
    public class WishlistViewModel
    {
        public int Id { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        public DateTimeOffset LastChangeDate { get; set; }

        public ICollection<WishlistItemViewModel> Items { get; set; } = new HashSet<WishlistItemViewModel>();

        public decimal Subtotal => Items.Sum(i => i.Product?.UnitPrice ?? default);

        public decimal Discount => Items.Sum(i => i.Product?.Discount ?? default);

        public decimal Total => Items.Sum(i => i.Product?.PriceAfterDiscount ?? default);

        public bool IsEmpty => Items.Count < 1;
    }

    public class WishlistItemViewModel
    {
        public int Id { get; set; }

        public int WishlistId { get; set; }

        public int ProductId { get; set; }

        public ProductViewModel? Product { get; set; }

        public int Quantity { get; set; }
    }
}
