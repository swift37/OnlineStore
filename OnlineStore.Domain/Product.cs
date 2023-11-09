using OnlineStore.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Domain
{
    public class Product : NamedEntity
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Discount { get; set; }

        [NotMapped]
        public decimal PriceBeforeDiscount => UnitPrice - Discount;

        public int UnitsInStock { get; set; }

        public string? Description { get; set; }

        [NotMapped]
        public string? ShortDescription => Description?.Length < 100 ? Description : Description?.Substring(0, 100);

        [NotMapped]
        public bool IsActive => UnitsInStock > 0;

        public string? Image { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public List<Specification> Specification { get; set; } = new List<Specification>();

        [Range(0.0, 5.0)]
        public double Rating { get; set; }

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