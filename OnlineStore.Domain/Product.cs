using OnlineStore.Domain.Base;

namespace OnlineStore.Domain
{
    public class Product : NamedEntity
    {
        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

        public string? Description { get; set; }

        public bool IsActive => UnitsInStock > 0;

        public Category? Category { get; set; }

        public SubCategory? SubCategory { get; set; }
    }
}
