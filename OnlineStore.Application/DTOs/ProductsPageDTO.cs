namespace OnlineStore.Application.DTOs
{
    public class ProductsPageDTO
    {
        public int Id { get; set; }

        public IEnumerable<ProductDTO> Products { get; set; } = Enumerable.Empty<ProductDTO>();

        public CategoryDTO? Category { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
