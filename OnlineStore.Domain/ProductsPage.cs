using OnlineStore.Domain.Base;

namespace OnlineStore.Domain
{
    public class ProductsPage : Entity
    {
        public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();

        public Category? Category { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
