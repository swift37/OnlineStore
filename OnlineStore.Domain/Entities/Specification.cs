using OnlineStore.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Domain.Entities
{
    public class Specification : Entity
    { 
        public int SpecificationTypeId { get; set; }

        public SpecificationType? SpecificationType { get; set; }

        public string? Value { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();

        public int ProductsCount { get; set; }
    }
}
