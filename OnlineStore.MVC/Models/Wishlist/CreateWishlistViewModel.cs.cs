using OnlineStore.MVC.Models.Product;

namespace OnlineStore.MVC.Models.Wishlist
{
    public class CreateWishlistViewModel
    {
        public ICollection<ProductViewModel> Products { get; set; } = new HashSet<ProductViewModel>();
    }
}
