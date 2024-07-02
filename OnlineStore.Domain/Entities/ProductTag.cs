using OnlineStore.Domain.Base;

namespace OnlineStore.Domain.Entities
{
    public class ProductTag : NamedEntity
    {
        public string? DisplayName { get; set; }

        public string? ColorHex { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
