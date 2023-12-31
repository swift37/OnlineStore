using OnlineStore.Application.DTOs.Product;
using OnlineStore.Domain;

namespace OnlineStore.Application.Mapping
{
    public static class ProductsMapper
    {
        public static ProductDTO ToDTO(this Product product) => new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            CategoryId = product.CategoryId,
            Category = product.Category?.ToDTO(),
            Description = product.Description,
            Discount = product.Discount,
            Image = product.Image,
            Specifications = product.Specifications.ToDTO(),
            UnitsInStock = product.UnitsInStock,
            UnitPrice = product.UnitPrice,
            Reviews = product.Reviews.ToDTO(),
            StoreCode = product.StoreCode,
            ManufacturersCode = product.ManufacturersCode,
            Manufacturer = product.Manufacturer,
            IsAvailable = product.IsAvailable,
            IsFeaturedProduct = product.IsFeaturedProduct,
            IsNewProduct = product.IsNewProduct,
            IsSale = product.IsSale,
        };

        public static Product FromDTO(this ProductDTO product) => new Product
        {
            Id = product.Id,
            Name = product.Name,
            CategoryId = product.CategoryId,
            Category = product.Category?.FromDTO(),
            Description = product.Description,
            Discount = product.Discount,
            Image = product.Image,
            Specifications = product.Specifications.FromDTO().ToArray(),
            UnitsInStock = product.UnitsInStock,
            UnitPrice = product.UnitPrice,
            Reviews = product.Reviews.FromDTO().ToArray(),
            StoreCode = product.StoreCode,
            ManufacturersCode = product.ManufacturersCode,
            Manufacturer = product.Manufacturer,
            IsAvailable = product.IsAvailable,
            IsFeaturedProduct = product.IsFeaturedProduct,
            IsNewProduct = product.IsNewProduct,
            IsSale = product.IsSale
        };

        public static Product FromDTO(this CreateProductDTO product) => new Product
        {
            Name = product.Name,
            CategoryId = product.CategoryId,
            Description = product.Description,
            Discount = product.Discount,
            Image = product.Image,
            Specifications = product.Specifications.FromDTO().ToArray(),
            UnitsInStock = product.UnitsInStock,
            UnitPrice = product.UnitPrice,
            StoreCode = product.StoreCode,
            ManufacturersCode = product.ManufacturersCode,
            Manufacturer = product.Manufacturer,
            IsAvailable = product.IsAvailable,
            IsFeaturedProduct = product.IsFeaturedProduct,
            IsNewProduct = product.IsNewProduct,
            IsSale = product.IsSale
        };

        public static Product FromDTO(this UpdateProductDTO product) => new Product
        {
            Id = product.Id,
            Name = product.Name,
            CategoryId = product.CategoryId,
            Description = product.Description,
            Discount = product.Discount,
            Image = product.Image,
            Specifications = product.Specifications.FromDTO().ToArray(),
            UnitsInStock = product.UnitsInStock,
            UnitPrice = product.UnitPrice,
            StoreCode = product.StoreCode,
            ManufacturersCode = product.ManufacturersCode,
            Manufacturer = product.Manufacturer,
            IsAvailable = product.IsAvailable,
            IsFeaturedProduct = product.IsFeaturedProduct,
            IsNewProduct = product.IsNewProduct,
            IsSale = product.IsSale
        };

        public static IEnumerable<ProductDTO> ToDTO(this IEnumerable<Product> products) => products.Select(p => p.ToDTO());

        public static IEnumerable<Product> FromDTO(this IEnumerable<ProductDTO> products) => products.Select(p => p.FromDTO());
    }
}
