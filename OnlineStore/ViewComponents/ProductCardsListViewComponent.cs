using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.ViewComponents
{
    public class ProductCardsListViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(IEnumerable<Product> products) =>
            Task.FromResult<IViewComponentResult>(View(products));
    }
}
