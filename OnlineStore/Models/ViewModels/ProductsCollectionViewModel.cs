using OnlineStore.Domain;

namespace OnlineStore.Models.ViewModels
{
    public class ProductsCollectionViewModel
    {
        public ProductsCollectionViewModel(
            IEnumerable<Product> products,
            Category? category,
            int currentPage,
            int totalPages,
            int itemsPerPage)
        {
            Products = products;
            Category = category;
            CurrentPage = currentPage;
            TotalPages = totalPages;
            ItemsPerPage = itemsPerPage;
        }

        public IEnumerable<Product> Products { get; set; }

        public int CategoryId => Category?.Id ?? -1;

        public Category? Category { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
