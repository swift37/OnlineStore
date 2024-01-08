using OnlineStore.MVC.Models.SpecificationType;

namespace OnlineStore.MVC.Models.FiltersGroup
{
    public class CreateFiltersGroupViewModel
    {
        public int CategoryId { get; set; }

        public ICollection<SpecificationTypeViewModel> SpecificationTypes { get; set; } = new HashSet<SpecificationTypeViewModel>();
    }
}
