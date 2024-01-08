using OnlineStore.MVC.Models.Category;
using OnlineStore.MVC.Models.SpecificationType;

namespace OnlineStore.MVC.Models.FiltersGroup
{
    public class FiltersGroupViewModel
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public CategoryViewModel? Category { get; set; }

        public ICollection<SpecificationTypeViewModel> SpecificationTypes { get; set; } = new HashSet<SpecificationTypeViewModel>();
    }
}
