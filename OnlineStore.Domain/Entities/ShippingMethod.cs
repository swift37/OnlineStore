using OnlineStore.Domain.Base;

namespace OnlineStore.Domain.Entities
{
    public class ShippingMethod : NamedEntity
    {
        public string? DisplayName { get; set; }

        public decimal Price { get; set; }

        public string? Image { get; set; }

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();

        public bool IsAvailable { get; set; } = true;
    }
}
