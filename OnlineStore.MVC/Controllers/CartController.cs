using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Models;
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
        public async Task<IActionResult> Checkout()
        {
            var cart = _cartStorage.Cart;

            if (cart is null) return NotFound();

            foreach (var item in cart.Items)
                item.Product = (await _productsService.Get(item.ProductId)).Data;

            var model = new CheckoutViewModel() { Cart = cart };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var cart = _cartStorage.Cart!;
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
                return RedirectToAction("Payment", new { orderId = orderCreateResponse.Data });
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
                SuccessUrl = domain + $"/cart/paymentsuccess?sessionId=" + "{CHECKOUT_SESSION_ID}" + "&orderId=" + order.Id,
                CancelUrl = domain + "/cart/paymentfailure"
            };

            foreach (var item in order.Items)
            {
                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmountDecimal = item.Product?.UnitPrice * 100,
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

            if (session.Status == "open")
                return RedirectToAction("PaymentInProcess");

            if (session.PaymentStatus != "paid") 
                return RedirectToAction("PaymentFailure");

            // Saving order and customer details to the database.
            var total = session.AmountTotal.HasValue ? session.AmountTotal.Value : 0;
            var customerEmail = session.CustomerDetails.Email;
            var payDate = session.Created;

            var response = await _ordersService.Get(orderId);
            if (!response.Success) return StatusCode(response.Status);
            var order = response.Data;

            order.Status = OrderStatus.Paid;
            order.Total = total;
            order.Email = customerEmail;
            order.PayDate = payDate;

            var orderUpdateResponse = await _ordersService.Update(order);
            if (orderUpdateResponse.Success)
            {
                ViewBag.OrderNumber = order.Number;
                return View();
            }

            return StatusCode(orderUpdateResponse.Status);
        }

        [HttpGet]
        public IActionResult PaymentFailure() => View();
    }
}
