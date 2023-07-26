using OnlineStore.Domain;

namespace OnlineStore.Models
{
    public class ProductsListViewModel 
    {
        public ProductsListViewModel(
            IEnumerable<Product> products, 
            int currentPage, 
            int totalPages)
        {
            Products = products;
            CurrentPage = currentPage;
            TotalPages = totalPages;
        }

        public IEnumerable<Product> Products { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }
}
