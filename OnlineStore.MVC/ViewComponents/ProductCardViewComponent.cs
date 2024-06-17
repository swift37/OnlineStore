using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Models.Product;

namespace OnlineStore.MVC.ViewComponents
{
    public class ProductCardViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(ProductViewModel model, bool isLight = false)
        {
            ViewBag.isLight = isLight;

            return Task.FromResult<IViewComponentResult>(View(model));
        }
    }
}
