using OnlineStore.Application.DTOs.Product;
using OnlineStore.Domain;

namespace OnlineStore.Application.Mapping
{
    public static class ProductsFilteringOptionsMapper
    {
        public static ProductsFilteringOptionsDTO ToDTO(this ProductsFilteringOptions options) => new ProductsFilteringOptionsDTO
        {
            CategoryId = options.CategoryId,
            MinPrice = options.MinPrice,
            MaxPrice = options.MaxPrice,
            SpecificationIds = options.SpecificationIds,
            PageNumber = options.PageNumber,
            ItemsPerPage = options.ItemsPerPage,
            SortBy = options.SortBy
        };

        public static ProductsFilteringOptions FromDTO(this ProductsFilteringOptionsDTO options) => new ProductsFilteringOptions
        {
            CategoryId = options.CategoryId,
            MinPrice = options.MinPrice,
            MaxPrice = options.MaxPrice,
            SpecificationIds = options.SpecificationIds,
            PageNumber = options.PageNumber,
            ItemsPerPage = options.ItemsPerPage,
            SortBy = options.SortBy
        };

        public static IEnumerable<ProductsFilteringOptionsDTO> ToDTO(this IEnumerable<ProductsFilteringOptions> options) => options.Select(p => p.ToDTO());

        public static IEnumerable<ProductsFilteringOptions> FromDTO(this IEnumerable<ProductsFilteringOptionsDTO> options) => options.Select(p => p.FromDTO());
    }
}
