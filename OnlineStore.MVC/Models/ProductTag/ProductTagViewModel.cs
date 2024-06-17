using OnlineStore.MVC.Models.Product;

namespace OnlineStore.MVC.Models.ProductTag
{
    public class ProductTagViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? DisplayName { get; set; }

        public string? ColorHex { get; set; }

        public ICollection<ProductViewModel> Products { get; set; } = new HashSet<ProductViewModel>();
    }
}
