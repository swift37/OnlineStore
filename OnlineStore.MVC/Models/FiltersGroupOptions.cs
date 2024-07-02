namespace OnlineStore.MVC.Models
{
    public class FiltersGroupOptions
    {
        public int CategoryId { get; set; }

        public int AppliedMinPrice { get; set; }

        public int AppliedMaxPrice { get; set; }

        public IDictionary<int, ICollection<int>> AppliedFilters { get; set; } = new Dictionary<int, ICollection<int>>();
    }
}
