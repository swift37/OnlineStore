using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Domain;
using OnlineStore.Models;
using OnlineStore.Services;
using Stripe;
using Stripe.Checkout;

namespace OnlineStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailSenderService _emailSender;

        public OrderController(ApplicationDbContext context, EmailSenderService emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        public IActionResult ViewCart()
        {
            var cart = _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(c => c.Status == CartStatus.Active);

            return View(cart);
        }


        public IActionResult UpdateCart(int productId, int qty)
        {
            var cart = _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(c => c.Status == CartStatus.Active);

            var cartItem = cart?.CartItems.FirstOrDefault(i => i.Product?.Id == productId);

            if (cart is null || cartItem is null) return Json(null);

            cartItem.Quantity = qty;
            _context.SaveChanges();
            return Json(new
            {
                CartPrice = cart.TotalPrice,
                CartItems = cart.TotalQuantity,
                LinePrice = cartItem.Price
            });
        }

        public IActionResult PersonalInfo(int cartId)
        {
            ViewBag.Cart = cartId;
            return View();
        }

        public IActionResult CreateOrder(Order order)
        {
            var cart = _context.Carts.SingleOrDefault(x => x.Id == order.CartId);
            if (cart is null) return RedirectToAction("NotFound", "Error");
            order.Cart = cart;
            cart.Status = CartStatus.Completed;

            _context.Orders.Add(order);
            _context.SaveChanges();

            _emailSender.SendMail("To", "New order", "Message");

            return RedirectToAction("Payment");
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var stripePublishKey = "pk_test_51NactwFJ49wekJ8Os9PXUEqkNEx1uVNwNBDtkk9Z1THTOK9KLriuiIBOxdwgEvly1bdmquaTsuu3mvTdzapgePw7003wR3jSQQ";
            ViewBag.StripePublishKey = stripePublishKey;
            return View();
        }

        [HttpPost]
        public async Task<string> Checkout(Domain.Product product)
        {
            var domain = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmountDecimal = 210000/*product.UnitPrice*/,
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Name"/*product.Name*/,
                                Description = "Desc"/*product.Description*/,
                                //Images = new List<string?> { product.Image }
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                SuccessUrl = domain + $"/order/checkoutsuccess?sessionId=" + "{CHECKOUT_SESSION_ID}",
                CancelUrl = domain + "/order/checkoutfailed.html"
            };
            var service = new SessionService();
            Session session = await service.CreateAsync(options);

            Response.Headers.Add("Location", session.Url);
            return session.Id; //new StatusCodeResult(303);
        }

        public IActionResult CheckoutSuccess(string sessionId)
        {
            var sessionService = new SessionService();
            var session = sessionService.Get(sessionId);

            // Save order and customer details to your database.
            var total = session.AmountTotal.HasValue ? session.AmountTotal.Value : 0;
            var customerEmail = session.CustomerDetails.Email;

            return View();
        }

        public IActionResult CheckoutFailed()
        {
            return View();
        }
    }
}
