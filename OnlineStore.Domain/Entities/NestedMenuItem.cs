using OnlineStore.Domain.Base;

namespace OnlineStore.Domain.Entities
{
    public class NestedMenuItem : NamedEntity
    {
        public int ParentId { get; set; }

        public MenuItem? Parent { get; set; }

        public ICollection<Category> Categories { get; set; } = new HashSet<Category>();

        public bool HasTwoColumns { get; set; }
    }
}
