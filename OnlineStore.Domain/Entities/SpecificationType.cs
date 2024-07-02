using OnlineStore.Domain.Base;

namespace OnlineStore.Domain.Entities
{
    public class SpecificationType : NamedEntity
    {
        public string? DisplayName { get; set; }

        public bool IsMain { get; set; }

        public ICollection<Specification> Values { get; set; } = new HashSet<Specification>();
    }
}
