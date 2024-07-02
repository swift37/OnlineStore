using OnlineStore.MVC.Models.Category;

namespace OnlineStore.MVC.Models.Product
{
    public class ProductsPageViewModel
    {
        public ICollection<ProductViewModel> Products { get; set; } = new HashSet<ProductViewModel>();

        public CategoryViewModel? Category { get; set; }

        public int MinPrice { get; set; }

        public int MaxPrice { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
