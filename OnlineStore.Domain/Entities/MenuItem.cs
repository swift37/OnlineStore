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

    public class NestedMenuItem : NamedEntity
    {
        public int ParentId { get; set; }

        public MenuItem? Parent { get; set; }

        public ICollection<Category> Categories { get; set; } = new HashSet<Category>();

        public bool HasTwoColumns { get; set; }
    }
}
