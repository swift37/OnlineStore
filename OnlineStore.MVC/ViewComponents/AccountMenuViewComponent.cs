using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.MVC.ViewComponents
{
    public class AccountMenuViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.Current = id;
            return Task.FromResult<IViewComponentResult>(View());
        }
    }
}
