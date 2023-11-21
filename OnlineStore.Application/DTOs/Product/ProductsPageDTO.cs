using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Category;

namespace OnlineStore.Application.DTOs.Product
{
    public class ProductsPageDTO : BaseDTO
    {
        public IEnumerable<ProductDTO> Products { get; set; } = Enumerable.Empty<ProductDTO>();

        public CategoryDTO? Category { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
