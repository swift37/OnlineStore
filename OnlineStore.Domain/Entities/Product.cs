using OnlineStore.Domain.Base;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain
{
    public class Product : NamedEntity
    {
        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public int UnitsInStock { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public ICollection<Specification> Specifications { get; set; } = new HashSet<Specification>();

        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

        public string? Manufacturer { get; set; }

        public string? ManufacturersCode { get; set; }

        public string? StoreCode { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsNewProduct { get; set; }

        public bool IsSale { get; set; }

        public bool IsFeaturedProduct { get; set; }
    }
}