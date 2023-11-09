using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain;

namespace OnlineStore.ViewComponents
{
    public class MenuItemViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(MenuItem menuItem) => 
            Task.FromResult<IViewComponentResult>(View(menuItem));
    }
}
