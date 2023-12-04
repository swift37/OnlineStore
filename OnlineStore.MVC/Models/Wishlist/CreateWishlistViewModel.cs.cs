using OnlineStore.MVC.Models.Product;

namespace OnlineStore.MVC.Models.Wishlist
{
    public class CreateWishlistViewModel
    {
        public DateTimeOffset CreateDate { get; set; }

        public ICollection<ProductViewModel> Products { get; set; } = new HashSet<ProductViewModel>();
    }
}
