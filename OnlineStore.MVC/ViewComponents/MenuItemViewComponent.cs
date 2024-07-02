using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Models.MenuItem;

namespace OnlineStore.MVC.ViewComponents
{
    public class MenuItemViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(MenuItemViewModel model) => 
            Task.FromResult<IViewComponentResult>(View(model));
    }
}
