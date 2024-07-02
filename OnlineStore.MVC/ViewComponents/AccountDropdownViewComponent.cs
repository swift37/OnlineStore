using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.MVC.ViewComponents
{
    public class AccountDropdownViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(int id) => 
            Task.FromResult<IViewComponentResult>(View());
    }
}
