using OnlineStore.Domain.Base;

namespace OnlineStore.Domain.Entities
{
    public class FiltersGroup : Entity
    {
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public ICollection<SpecificationType> SpecificationTypes { get; set; } = new HashSet<SpecificationType>();
    }
}
