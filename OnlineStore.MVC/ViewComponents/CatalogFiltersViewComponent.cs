using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Models.FiltersGroup;

namespace OnlineStore.MVC.ViewComponents
{
    public class CatalogFiltersViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(FiltersGroupViewModel model) =>
            Task.FromResult<IViewComponentResult>(View(model));
    }
}
