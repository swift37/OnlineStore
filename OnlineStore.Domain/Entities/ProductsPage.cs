namespace OnlineStore.Domain.Entities
{
    public class ProductsPage
    {
        public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();

        public Category? Category { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
