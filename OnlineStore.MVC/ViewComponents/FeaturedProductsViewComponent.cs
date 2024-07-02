using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.ViewComponents
{
    public class FeaturedProductsViewComponent : ViewComponent
    {
        private readonly IProductsService _productsService;

        public FeaturedProductsViewComponent(IProductsService productsService) =>
            _productsService = productsService;

        public async Task<IViewComponentResult> InvokeAsync(int productId, string? text)
        {
            var response = await _productsService.GetAllByTag("featured");
            var result = response.Data;

            if (!result.Any())
                return Content(string.Empty);

            return View(result);
        }
    }
}
