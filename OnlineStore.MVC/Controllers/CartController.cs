using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Models.Enums;
using OnlineStore.MVC.Models.Order;
using OnlineStore.MVC.Services.Interfaces;
using Stripe.Checkout;

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
        public IActionResult Update(int productId, int quantity)
        {
            if (!_cartService.Update(productId, quantity))
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

        [HttpGet]
        public async Task<IActionResult> Checkout(int cartId)
        {
            var cart = _cartStorage.Cart;

            if (cart is null) return NotFound();

            foreach (var item in cart.Items)
                item.Product = (await _productsService.Get(item.ProductId)).Data;

            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CreateOrderViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _ordersService.Create(model);

            if (response.Success)
                return RedirectToAction("Payment", new { orderId = response.Data });

            if (response.Status == 400 && response.ValidationErrors.Count() > 0)
            {
                foreach (var error in response.ValidationErrors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(model);
            }

            return StatusCode(response.Status);
        }

        [HttpPost]
        public async Task<IActionResult> Payment(int orderId)
        {
            var response = await _ordersService.Get(orderId);
            if (!response.Success) return StatusCode(response.Status); ;
            var order = response.Data;

            var domain = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                PaymentMethodTypes = new List<string> { "card" },
                SuccessUrl = domain + $"/cart/checkoutsuccess?sessionId=" + "{CHECKOUT_SESSION_ID}" + "&orderId=" + order.Id,
                CancelUrl = domain + "/cart/checkoutfailed"
            };

            foreach (var item in order.Items)
            {
                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmountDecimal = item.Product?.UnitPrice,
                        Currency = "USD",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product?.Name,
                        }
                    },
                    Quantity = item.Quantity
                });
            }

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Append("Location", session.Url);
            return new StatusCodeResult(303);
        }

        [HttpGet]
        public async Task<IActionResult> PaymentSuccess(string sessionId, int orderId)
        {
            var sessionService = new SessionService();
            var session = sessionService.Get(sessionId);

            // Saving order and customer details to the database.
            var total = session.AmountTotal.HasValue ? session.AmountTotal.Value : 0;
            var customerEmail = session.CustomerDetails.Email;
            var payDate = session.Created;

            var response = await _ordersService.Get(orderId);
            if (!response.Success) return StatusCode(response.Status); ;
            var order = response.Data;

            order.Status = OrderStatus.Paid;
            order.Email = customerEmail;
            order.PayDate = payDate;

            if (!ModelState.IsValid) return View(order);

            var orderUpdateResponse = await _ordersService.Update(order);

            if (orderUpdateResponse.Success)
                return RedirectToAction("GetAll");

            if (orderUpdateResponse.Status == 400 && orderUpdateResponse.ValidationErrors.Count() > 0)
            {
                foreach (var error in orderUpdateResponse.ValidationErrors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View();
            }

            return StatusCode(orderUpdateResponse.Status);
        }

        [HttpGet]
        public IActionResult PaymentFailure() => View();

    }
}
