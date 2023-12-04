using OnlineStore.MVC.Models.Product;

namespace OnlineStore.MVC.Models.Wishlist
{
    public class WishlistViewModel
    {
        public int Id { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        public DateTimeOffset LastChangeDate { get; set; }

        public ICollection<ProductViewModel> Products { get; set; } = new HashSet<ProductViewModel>();
    }
}
