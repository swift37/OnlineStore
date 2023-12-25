using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.MVC.ViewComponents
{
    public class AccountMenuViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync() =>
            Task.FromResult<IViewComponentResult>(View());
    }
}
