using OnlineStore.Domain.Base;
using OnlineStore.Domain.Enums;

namespace OnlineStore.Domain.Entities
{
    public class Product : NamedEntity
    {
        public decimal UnitCost { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public int UnitsInStock { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public ICollection<Specification> Specifications { get; set; } = new HashSet<Specification>();
        
        public ICollection<ProductTag> Tags { get; set; } = new HashSet<ProductTag>();

        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

        public string? Manufacturer { get; set; }

        public string? ManufacturersCode { get; set; }

        public string? StoreCode { get; set; }

        public ProductStatus Status { get; set; }

        public ProductAvailability Availability { get; set; }
    }
}