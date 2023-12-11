using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Models.Product;

namespace OnlineStore.MVC.ViewComponents
{
    public class ProductCardsListViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(IEnumerable<ProductViewModel> model) =>
            Task.FromResult<IViewComponentResult>(View(model));
    }
}
