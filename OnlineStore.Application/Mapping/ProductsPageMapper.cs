using OnlineStore.Application.DTOs.Product;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Mapping
{
    public static class ProductsPageMapper
    {
        public static ProductsPageDTO ToDTO(this ProductsPage productsPage) => new ProductsPageDTO
        {
            Products = productsPage.Products.ToDTO().ToArray(),
            Category = productsPage.Category?.ToDTO(),
            CurrentPage = productsPage.CurrentPage,
            TotalPages = productsPage.TotalPages,
            ItemsPerPage = productsPage.ItemsPerPage
        };

        public static ProductsPage FromDTO(this ProductsPageDTO productsPage) => new ProductsPage
        {
            Products = productsPage.Products.FromDTO().ToArray(),
            Category = productsPage.Category?.FromDTO(),
            CurrentPage = productsPage.CurrentPage,
            TotalPages = productsPage.TotalPages,
            ItemsPerPage = productsPage.ItemsPerPage
        };

        public static IEnumerable<ProductsPageDTO> ToDTO(this IEnumerable<ProductsPage> productsPage) => productsPage.Select(p => p.ToDTO());

        public static IEnumerable<ProductsPage> FromDTO(this IEnumerable<ProductsPageDTO> productsPage) => productsPage.Select(p => p.FromDTO());
    }
}
