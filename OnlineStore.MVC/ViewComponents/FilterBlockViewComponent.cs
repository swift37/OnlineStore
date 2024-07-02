using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Extensions;
using OnlineStore.MVC.Models;
using OnlineStore.MVC.Models.SpecificationType;

namespace OnlineStore.MVC.ViewComponents
{
    public class FilterBlockViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(SpecificationTypeViewModel model, bool showAll = false)
        {
            var filterBlock = new FilterBlockViewModel
            {
                SpecificationType = model,
                ShowAll = showAll,
                AppliedFilterIds = HttpContext.Request.Query["filters"].GetAppliedFilterIds(model.Id)
            };

            return Task.FromResult<IViewComponentResult>(View(filterBlock));
        }
    }
}
