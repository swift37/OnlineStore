using OnlineStore.MVC.Models.FiltersGroup;
using OnlineStore.MVC.Models.Product;

namespace OnlineStore.MVC.Models
{
    public class CatalogViewModel
    {
        public FiltersGroupViewModel? FiltersGroup { get; set; }

        public ProductsPageViewModel? ProductsPage { get; set; }
    }
}
