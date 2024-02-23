using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Models;
using OnlineStore.MVC.Models.Cart;
using OnlineStore.MVC.Models.Order;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartStorage _cartStorage;
        private readonly ICartService _cartService;
        private readonly IProductsService _productsService;
        private readonly IOrdersService _ordersService;

        public CartController(
            ICartStorage cartStorage, 
            ICartService cartService, 
            IProductsService productsService,
            IOrdersService ordersService) => 
            (_cartStorage, _cartService, _productsService, _ordersService) = 
            (cartStorage, cartService, productsService, ordersService);

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
        public IActionResult Add(int productId, int quantity)
        {
            if (!_cartService.Add(productId, quantity))
                return Json(new { success = false });

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult AddRange(IEnumerable<CartItemViewModel> items)
        {
            foreach (var item in items)
                if (!_cartService.Add(item.ProductId, item.Quantity))
                    return Json(new { success = false });

            return Json(new { success = true });
        }

        [HttpPut]
        public IActionResult Update(int productId, int quantity)
        {
            if (!_cartService.Update(productId, quantity))
                return Json(new { success = false });

            return Json(new { success = true });
        }

        [HttpDelete]
        public IActionResult Remove(int productId)
        {
            if (!_cartService.Remove(productId))
                return Json(new { success = false });

            return Json(new { success = true });
        }

        [HttpDelete]
        public IActionResult Clear()
        {
            _cartService.Clear();
            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult UpdateMiniCart() => ViewComponent("MiniCart");

        [HttpGet("cart/checkout/login")]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated is true) return RedirectToAction("Checkout");

            var model = new LoginViewModel() 
            { 
                RedirectUrl = Url.Action("Checkout", "Cart", new { useUnauthCart = true }) 
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Checkout(bool useUnauthCart = false)
        {
            var cart = useUnauthCart ? _cartStorage.GetUnauthCart() : _cartStorage.Cart;
            if (cart is null) return NotFound();

            foreach (var item in cart.Items)
                item.Product = (await _productsService.Get(item.ProductId)).Data;

            var model = new CheckoutViewModel() { Cart = cart, UseUnauthCart = useUnauthCart };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var cart = model.UseUnauthCart ? _cartStorage.GetUnauthCart() : _cartStorage.Cart;
            if (cart is null) return NotFound();

            var order = model.Order;

            foreach (var item in cart.Items)
            {
                var checkProductResponse = await _productsService.Exist(item.ProductId);
                if (!checkProductResponse.Success) return StatusCode(checkProductResponse.Status);
                if (!checkProductResponse.Data) continue;

                order.Items.Add(new CreateOrderItemViewModel 
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                });
            }

            var orderCreateResponse = await _ordersService.Create(order);

            if (orderCreateResponse.Success)
            {
                _cartService.Clear();
                return RedirectToAction("Payment", new { orderNumber = orderCreateResponse.Data });
            }

            if (orderCreateResponse.Status == 400 && orderCreateResponse.ValidationErrors.Count() > 0)
            {
                foreach (var error in orderCreateResponse.ValidationErrors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(model);
            }

            return StatusCode(orderCreateResponse.Status);
        }

        [HttpGet]
        public async Task<IActionResult> Payment(string orderNumber)
        {
            var domain = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

            var request = new StripePaymentRequest
            {
                OrderNumber = orderNumber,
                SuccessUrl = Url.Action("PaymentSuccess", "Cart", new { orderNumber }, Request.Scheme)!,
                CancelUrl = Url.Action("PaymentFailure", "Cart", new { orderNumber }, Request.Scheme)!,
            };

            var response = await _ordersService.StripePayment(request);
            if (!response.Success) return StatusCode(response.Status); ;

            Response.Headers.Append("Location", response.Data.SessionUrl);
            return new StatusCodeResult(303);
        }

        [HttpGet("cart/payment-success")]
        public async Task<IActionResult> PaymentSuccess(string orderNumber)
        {
            var response = await _ordersService.ConfirmStripePayment(orderNumber);
            if (!response.Success) return StatusCode(response.Status);
            var paymentStatus = response.Data;

            if (paymentStatus.IsInProcess)
                return RedirectToAction("PaymentInProcess", new { orderNumber });
            if (paymentStatus.IsFailed)
                return RedirectToAction("PaymentFailure", new { orderNumber });

            if (paymentStatus.IsPaid)
            {
                ViewBag.OrderNumber = orderNumber;
                return View();
            }

            return StatusCode(500);
        }

        [HttpGet("cart/payment-in-process")]
        public IActionResult PaymentInProcess(string orderNumber)
        {
            ViewBag.OrderNumber = orderNumber;
            return View();
        }

        [HttpGet("cart/payment-failure")]
        public IActionResult PaymentFailure(string orderNumber)
        {
            ViewBag.OrderNumber = orderNumber;
            return View();
        }
    }
}
