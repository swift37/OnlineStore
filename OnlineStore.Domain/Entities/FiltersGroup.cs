using OnlineStore.Domain.Base;

namespace OnlineStore.Domain.Entities
{
    public class FiltersGroup : Entity
    {
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public int MinPrice { get; set; }

        public int MaxPrice { get; set; }

        public int AppliedMinPrice { get; set; }

        public int AppliedMaxPrice { get; set; }

        public ICollection<SpecificationType> SpecificationTypes { get; set; } = new HashSet<SpecificationType>();
    }
}
