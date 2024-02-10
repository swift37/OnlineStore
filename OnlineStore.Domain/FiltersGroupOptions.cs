namespace OnlineStore.Domain
{
    public class FiltersGroupOptions
    {
        public int CategoryId { get; set; }

        public IDictionary<int, ICollection<int>> AppliedFilters { get; set; } = new Dictionary<int, ICollection<int>>();
    }
}
