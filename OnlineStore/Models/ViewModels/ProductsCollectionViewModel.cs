using OnlineStore.Domain;

namespace OnlineStore.Models.ViewModels
{
    public class ProductsCollectionViewModel
    {
        public ProductsCollectionViewModel(
            IEnumerable<Product> products,
            Category? category,
            SubCategory? subCategory,
            int currentPage,
            int totalPages,
            int itemsPerPage)
        {
            Products = products;
            Category = category;
            SubCategory = subCategory;
            CurrentPage = currentPage;
            TotalPages = totalPages;
            ItemsPerPage = itemsPerPage;
        }

        public IEnumerable<Product> Products { get; set; }

        public Category? Category { get; set; }

        public SubCategory? SubCategory { get; set; }

        public int SubCategoryId => SubCategory?.Id ?? -1;

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
