using OnlineStore.Domain.Base;

namespace OnlineStore.Domain.Entities
{
    public class MenuItem : NamedEntity
    {
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public bool IsMegaMenu { get; set; }

        public ICollection<NestedMenuItem> NestedItems { get; set; } = new HashSet<NestedMenuItem>();

        public string? Image { get; set; }
    }
}
