using OnlineStore.MVC.Models.Specification;

namespace OnlineStore.MVC.Models
{
    public class AppliedFilterWrapViewModel
    {
        
        public IEnumerable<IGrouping<string, SpecificationViewModel>> AppliedFilters { get; set; } = Enumerable.Empty<IGrouping<string, SpecificationViewModel>>();

        public bool IsEmpty => AppliedFilters.Any() is false;

        public int Count => AppliedFilters.Count();
    }
}
