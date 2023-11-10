using OnlineStore.Application.Models;
using OnlineStore.Domain;

namespace OnlineStore.Application.Mapping
{
    public static class ProductsMapper
    {
        public static ProductDTO? ToDTO(this Product product) => product is null ? null : new ProductDTO
        {
            Name = product.Name,
            Category = product.Category?.ToDTO(),
            Description = product.Description,
            Discount = product.Discount,
            Image = product.Image,
            Specifications = product.Specifications.ToDTO(),
            UnitsInStock = product.UnitsInStock,
            UnitPrice = product.UnitPrice,
            Rating = product.Rating,
            Reviews = product.Reviews.ToDTO(),
            StoreCode = product.StoreCode,
            ManufacturersCode = product.ManufacturersCode,
            Manufacturer = product.Manufacturer,
            IsAvailable = product.IsAvailable,
            IsFeaturedProduct = product.IsFeaturedProduct,
            IsNewProduct = product.IsNewProduct,
            IsSale = product.IsSale,
        };

        public static Product? FromDTO(this ProductDTO product) => product is null ? null : new Product
        {
            Name = product.Name,
            Category = product.Category?.FromDTO(),
            Description = product.Description,
            Discount = product.Discount,
            Image = product.Image,
            Specifications = product.Specifications.FromDTO().ToList(),
            UnitsInStock = product.UnitsInStock,
            UnitPrice = product.UnitPrice,
            Rating = product.Rating,
            Reviews = product.Reviews.FromDTO().ToList(),
            StoreCode = product.StoreCode,
            ManufacturersCode = product.ManufacturersCode,
            Manufacturer = product.Manufacturer,
            IsAvailable = product.IsAvailable,
            IsFeaturedProduct = product.IsFeaturedProduct,
            IsNewProduct = product.IsNewProduct,
            IsSale = product.IsSale
        };

        public static IEnumerable<ProductDTO?> ToDTO(this IEnumerable<Product?> products) => products.Select(p => p?.ToDTO());

        public static IEnumerable<Product?> FromDTO(this IEnumerable<ProductDTO?> products) => products.Select(p => p?.FromDTO());
    }
}
