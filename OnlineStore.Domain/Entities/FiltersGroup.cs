using OnlineStore.Domain.Base;

namespace OnlineStore.Domain.Entities
{
    public class FiltersGroup : Entity
    {
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public ICollection<Specification> Specifications { get; set; } = new HashSet<Specification>();
    }
}
