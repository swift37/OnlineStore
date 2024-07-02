using OnlineStore.MVC.Models.Specification;
using OnlineStore.MVC.Models.SpecificationType;

namespace OnlineStore.MVC.Models
{
    public class FilterBlockViewModel
    {
        public SpecificationTypeViewModel? SpecificationType { get; set; }

        public ICollection<int> AppliedFilterIds { get; set; } = new HashSet<int>();

        public bool ShowAll { get; set; }

        public bool IsEmpty => SpecificationType?.Values.Any() is false;

        public int FilterCount => SpecificationType?.Values.Count() ?? default;

        public IList<SpecificationViewModel> Filters => 
            SpecificationType?.Values.ToList() ?? new List<SpecificationViewModel>();
    }
}
