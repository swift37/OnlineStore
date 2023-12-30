namespace OnlineStore.Domain.Entities
{
    public class ProductsPage
    {
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();

        public Category? Category { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
