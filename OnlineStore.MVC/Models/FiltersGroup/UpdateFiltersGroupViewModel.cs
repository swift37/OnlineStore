using OnlineStore.MVC.Models.Category;

namespace OnlineStore.MVC.Models.FiltersGroup
{
    public class UpdateFiltersGroupViewModel
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public CategoryViewModel? Category { get; set; }

        public ICollection<int> SpecificationTypeIds { get; set; } = new HashSet<int>();
    }
}
