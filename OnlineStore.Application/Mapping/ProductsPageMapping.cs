using OnlineStore.Application.Models;
using OnlineStore.Domain;

namespace OnlineStore.Application.Mapping
{
    public static class ProductsPageMapping
    {
        public static ProductsPageDTO? ToDTO(this ProductsPage productsPage) => productsPage is null ? null : new ProductsPageDTO
        {
            Id = productsPage.Id,
            Products = productsPage.Products.ToDTO(),
            Category = productsPage.Category?.ToDTO(),
            CurrentPage = productsPage.CurrentPage,
            TotalPages = productsPage.TotalPages,
            ItemsPerPage = productsPage.ItemsPerPage
        };

        public static ProductsPage? FromDTO(this ProductsPageDTO productsPage) => productsPage is null ? null : new ProductsPage
        {
            Id = productsPage.Id,
            Products = productsPage.Products.FromDTO(),
            Category = productsPage.Category?.FromDTO(),
            CurrentPage = productsPage.CurrentPage,
            TotalPages = productsPage.TotalPages,
            ItemsPerPage = productsPage.ItemsPerPage
        };

        public static IEnumerable<ProductsPageDTO?> ToDTO(this IEnumerable<ProductsPage?> productsPage) => productsPage.Select(p => p?.ToDTO());

        public static IEnumerable<ProductsPage?> FromDTO(this IEnumerable<ProductsPageDTO?> productsPage) => productsPage.Select(p => p?.FromDTO());
    }
}
