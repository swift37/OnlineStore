using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartStorage _cartStorage;
        private readonly ICartService _cartService;
        private readonly IProductsService _productsService;

        public CartController(ICartStorage cartStorage, ICartService cartService, IProductsService productsService) => 
            (_cartStorage, _cartService, _productsService) = (cartStorage, cartService, productsService);

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var cart = _cartStorage.Cart;

            if (cart is null) return NotFound();

            foreach (var item in cart.Items)
                item.Product = (await _productsService.Get(item.ProductId)).Data;

            return View(cart);
        }

        [HttpPost]
        public IActionResult Add(int productId)
        {
            if (!_cartService.Add(productId))
                return Json(new { success = false });

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Decrement(int productId)
        {
            if (!_cartService.Decrement(productId))
                return Json(new { success = false });

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Remove(int productId)
        {
            if (!_cartService.Remove(productId))
                return Json(new { success = false });

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Clear()
        {
            _cartService.Clear();
            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult UpdateMiniCart() => ViewComponent("MiniCart");
    }
}
