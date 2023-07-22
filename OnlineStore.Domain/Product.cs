using OnlineStore.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Domain
{
    public class Product : NamedEntity
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

        public string? Description { get; set; }

        [NotMapped]
        public bool IsActive => UnitsInStock > 0;

        public string? Image { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public int SubCategoryId { get; set; }

        public SubCategory? SubCategory { get; set; }

        public ICollection<ProductDetails> ProductDetails { get; set; } = new HashSet<ProductDetails>();
    }
}
