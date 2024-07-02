using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Models.Specification;
using OnlineStore.MVC.Models;

namespace OnlineStore.MVC.ViewComponents
{
    public class AppliedFiltersWrapViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(IEnumerable<SpecificationViewModel> model)
        {
            var filterBlock = new AppliedFilterWrapViewModel
            {
                AppliedFilters = model.GroupBy(s => s.SpecificationType?.DisplayName ?? string.Empty)
            };

            return Task.FromResult<IViewComponentResult>(View(filterBlock));
        }
    }
}
