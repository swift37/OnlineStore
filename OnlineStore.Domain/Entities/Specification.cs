using OnlineStore.Domain.Base;

namespace OnlineStore.Domain.Entities
{
    public class Specification : Entity
    { 
        public int SpecificationTypeId { get; set; }

        public SpecificationType? SpecificationType { get; set; }

        public string? Value { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
