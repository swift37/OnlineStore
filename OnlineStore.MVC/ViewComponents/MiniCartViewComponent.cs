using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.ViewComponents
{
    public class MiniCartViewComponent : ViewComponent
    {
        private readonly ICartStorage _cartStorage;
        private readonly IProductsService _productsService;

        public MiniCartViewComponent(
            ICartStorage cartStorage,
            IProductsService productsService) => 
            (_cartStorage, _productsService) = (cartStorage, productsService);

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _cartStorage.Cart;

            if (model is { } && model.Items.Count > 0)
                foreach (var item in model.Items)
                    item.Product = (await _productsService.Get(item.ProductId)).Data;

            return View(model);
        }
    }
}
