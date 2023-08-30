using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Domain;
using OnlineStore.Services;
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

        [Route("cart")]
        public IActionResult ViewCart()
        {
            var cart = _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(c => c.Status == CartStatus.Active);

            return View(cart);
        }


        public async Task<IActionResult> UpdateCart(int productId, int qty)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.Status == CartStatus.Active);

            var cartItem = cart?.CartItems.FirstOrDefault(i => i.Product?.Id == productId);

            if (cart is null || cartItem is null) return Json(new { error = true, message = "Error occurred." });

            cartItem.Quantity = qty;
            await _context.SaveChangesAsync();
            return Json(new
            {
                error = false,
                SubtotalPrice = cart.SubtotalPrice,
                TotalPrice = cart.TotalPrice,
                TotalDiscount = cart.TotalDiscount,
                TotalQuantity = cart.TotalQuantity,
                LinePrice = cartItem.Price
            });
        }

        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.Status == CartStatus.Active);

            var cartItem = cart?.CartItems.SingleOrDefault(p => p.ProductId == productId);

            if (cart is null || cartItem is null) return Json( new { removeSuccess = false } );

            cart.CartItems.Remove(cartItem);
            _context.Entry(cart).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Json(new { removeSuccess = true } );
        }

        public IActionResult PersonalInfo(int cartId)
        {
            ViewBag.Cart = cartId;
            return View();
        }

        public IActionResult CreateOrder(Order order)
        {
            var cart = _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(i => i.Product)
                .SingleOrDefault(x => x.Id == order.CartId);
            if (cart is null) return RedirectToAction("NotFound", "Error");
            order.Cart = cart;
            cart.Status = CartStatus.Completed;
            cart.PayDate = DateTime.Now;

            _context.Orders.Add(order);
            _context.SaveChanges();

            //_emailSender.SendMail("To", "New order", "Message");

            return RedirectToAction("Checkout", new { cartId = cart.Id });
        }

        //[HttpGet]
        //public IActionResult Checkout(Cart cart)
        //{
        //    return View(cart);
        //}

        public IActionResult Checkout(int cartId)
        {
            var cart = _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(i => i.Product)
                .SingleOrDefault(c => c.Id == cartId);
            if (cart is null) return RedirectToAction("NotFound", "Error");

            var domain = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                PaymentMethodTypes = new List<string>{ "card" },
                SuccessUrl = domain + $"/order/checkoutsuccess?sessionId=" + "{CHECKOUT_SESSION_ID}" + "&cartId=" + cart.Id,
                CancelUrl = domain + "/order/checkoutfailed.html"
            };

            foreach (var cartItem in cart.CartItems)
            {
                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmountDecimal = cartItem.Product?.UnitPrice,
                        Currency = "USD",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = cartItem.Product?.Name,
                        }
                    },
                    Quantity = cartItem.Quantity
                });
            }

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        public IActionResult CheckoutSuccess(string sessionId, int cartId)
        {
            var sessionService = new SessionService();
            var session = sessionService.Get(sessionId);

            // Save order and customer details to your database.
            var total = session.AmountTotal.HasValue ? session.AmountTotal.Value : 0;
            var customerEmail = session.CustomerDetails.Email;

            var order = _context.Orders.SingleOrDefault(o => o.CartId == cartId);

            if (order is null) return RedirectToAction("NotFound", "Error");

            order.Status = OrderStatus.Paid;
            order.Email = customerEmail;
            order.Total = total / 100;

            _context.SaveChanges();

            return View();
        }

        public IActionResult CheckoutFailed()
        {
            return View();
        }
    }
}
