namespace OnlineStore.Domain
{
    public class SpecificationTypeOptions
    {
        public int Id { get; set; }

        public IDictionary<int, ICollection<int>> AppliedFilters { get; set; } = new Dictionary<int, ICollection<int>>();
    }
}
