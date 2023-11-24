using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Category;
using OnlineStore.Application.DTOs.Review;

namespace OnlineStore.Application.DTOs.Product
{
    public class UpdateProductDTO : BaseDTO
    {
        public string? Name { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public int UnitsInStock { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<UpdateSpecificationDTO> Specifications { get; set; } = Enumerable.Empty<UpdateSpecificationDTO>();

        public double Rating { get; set; }

        public string? Manufacturer { get; set; }

        public string? ManufacturersCode { get; set; }

        public string? StoreCode { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsNewProduct { get; set; }

        public bool IsSale { get; set; }

        public bool IsFeaturedProduct { get; set; }
    }

    public class UpdateSpecificationDTO : BaseDTO
    {
        public string? Name { get; set; }

        public string? Value { get; set; }

        public bool IsMain { get; set; }
    }
}
