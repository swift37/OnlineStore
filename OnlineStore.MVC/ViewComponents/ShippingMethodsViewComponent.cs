using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.ViewComponents
{
    public class ShippingMethodsViewComponent : ViewComponent
    {
        private readonly IShippingMethodsService _shippingMethodsService;

        public ShippingMethodsViewComponent(IShippingMethodsService shippingMethodsService) =>
            _shippingMethodsService = shippingMethodsService;

        public async Task<IViewComponentResult> InvokeAsync(int productId, string? text)
        {
            var response = await _shippingMethodsService.GetAll();
            var result = response.Data.Where(method => method.IsAvailable);

            return View(result);
        }
    }
}
