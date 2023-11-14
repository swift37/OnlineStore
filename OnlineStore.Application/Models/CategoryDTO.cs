namespace OnlineStore.Application.Models
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public CategoryDTO? Parent { get; set; }

        public IEnumerable<CategoryDTO> Subcategories { get; set; } = Enumerable.Empty<CategoryDTO>();

        public IEnumerable<ProductDTO> Products { get; set; } = Enumerable.Empty<ProductDTO>();

        public bool IsMainCategory { get; set; }
    }
}
