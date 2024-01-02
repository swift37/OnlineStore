using OnlineStore.Domain.Base;

namespace OnlineStore.Domain.Entities
{
    public class Specification : NamedEntity
    {
        public string? Value { get; set; }

        public bool IsMain { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
