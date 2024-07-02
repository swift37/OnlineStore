using OnlineStore.MVC.Models.Category;
using OnlineStore.MVC.Models.SpecificationType;

namespace OnlineStore.MVC.Models.FiltersGroup
{
    public class FiltersGroupViewModel
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public CategoryViewModel? Category { get; set; }

        public int MinPrice { get; set; }

        public int MaxPrice { get; set; }

        public int AppliedMinPrice { get; set; }

        public int AppliedMaxPrice { get; set; }

        public ICollection<SpecificationTypeViewModel> SpecificationTypes { get; set; } = new HashSet<SpecificationTypeViewModel>();
    }
}
