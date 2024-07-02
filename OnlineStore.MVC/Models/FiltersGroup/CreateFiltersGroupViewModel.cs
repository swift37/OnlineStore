namespace OnlineStore.MVC.Models.FiltersGroup
{
    public class CreateFiltersGroupViewModel
    {
        public int CategoryId { get; set; }

        public ICollection<int> SpecificationTypeIds { get; set; } = new HashSet<int>();
    }
}
