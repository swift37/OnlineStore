using OnlineStore.MVC.Models.Product;

namespace OnlineStore.MVC.Models.Wishlist
{
    public class WishlistViewModel
    {
        public int Id { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        public DateTimeOffset LastChangeDate { get; set; }

        public ICollection<ProductViewModel> Products { get; set; } = new HashSet<ProductViewModel>();

        public decimal Subtotal => Products.Sum(i => i.UnitPrice);

        public decimal Discount => Products.Sum(i => i.Discount);

        public decimal Total => Products.Sum(i => i.PriceAfterDiscount);

        public bool IsEmpty => Products.Count < 1;
    }
}
