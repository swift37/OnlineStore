using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain;

namespace OnlineStore.ViewComponents
{
    public class ProductCardViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(Product product) => 
            Task.FromResult<IViewComponentResult>(View(product));
    }
}
