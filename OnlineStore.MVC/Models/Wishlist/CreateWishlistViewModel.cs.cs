namespace OnlineStore.MVC.Models.Wishlist
{
    public class CreateWishlistViewModel
    {
        public ICollection<CreateWishlistItemViewModel> Items { get; set; } = new HashSet<CreateWishlistItemViewModel>();
    }

    public class CreateWishlistItemViewModel
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
