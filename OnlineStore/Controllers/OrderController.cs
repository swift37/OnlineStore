using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.Context;
using OnlineStore.Domain;
using Stripe.Checkout;

namespace OnlineStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        //public IActionResult PersonalInfo(int cartId)
        //{
        //    var cart = _context.Carts
        //        .Include(c => c.CartItems)
        //        .ThenInclude(i => i.Product)
        //        .FirstOrDefault(c => c.Status == CartStatus.Active);

        //    return View(cart);
        //}

        //public IActionResult CreateOrder(Order order)
        //{
        //    var cart = _context.Carts
        //        .Include(c => c.CartItems)
        //        .ThenInclude(i => i.Product)
        //        .SingleOrDefault(x => x.Id == order.CartId);
        //    if (cart is null) return RedirectToAction("NotFound", "Error");
        //    order.Cart = cart;
        //    order.Total = cart.TotalPrice;
        //    cart.Status = CartStatus.Completed;
        //    cart.PayDate = DateTime.Now;

        //    _context.Orders.Add(order);
        //    _context.SaveChanges();

        //    //_emailSender.SendMail("To", "New order", "Message");

        //    //return RedirectToAction("Checkout", new { cartId = cart.Id });
        //    return Checkout(cart);
        //}

        //public IActionResult Checkout(Cart cart)
        //{
        //    if (cart is null) return RedirectToAction("NotFound", "Error");

        //    var domain = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
        //    var options = new SessionCreateOptions
        //    {
        //        LineItems = new List<SessionLineItemOptions>(),
        //        Mode = "payment",
        //        PaymentMethodTypes = new List<string>{ "card" },
        //        SuccessUrl = domain + $"/order/checkoutsuccess?sessionId=" + "{CHECKOUT_SESSION_ID}" + "&cartId=" + cart.Id,
        //        CancelUrl = domain + "/order/checkoutfailed.html"
        //    };

        //    foreach (var cartItem in cart.CartItems)
        //    {
        //        options.LineItems.Add(new SessionLineItemOptions
        //        {
        //            PriceData = new SessionLineItemPriceDataOptions
        //            {
        //                UnitAmountDecimal = cartItem.Product?.UnitPrice,
        //                Currency = "USD",
        //                ProductData = new SessionLineItemPriceDataProductDataOptions
        //                {
        //                    Name = cartItem.Product?.Name,
        //                }
        //            },
        //            Quantity = cartItem.Quantity
        //        });
        //    }

        //    var service = new SessionService();
        //    Session session = service.Create(options);

        //    Response.Headers.Add("Location", session.Url);
        //    return new StatusCodeResult(303);
        //}

        //public IActionResult CheckoutSuccess(string sessionId, int cartId)
        //{
        //    var sessionService = new SessionService();
        //    var session = sessionService.Get(sessionId);

        //    // Save order and customer details to your database.
        //    var total = session.AmountTotal.HasValue ? session.AmountTotal.Value : 0;
        //    var customerEmail = session.CustomerDetails.Email;

        //    var order = _context.Orders.SingleOrDefault(o => o.CartId == cartId);

        //    if (order is null) return RedirectToAction("NotFound", "Error");

        //    order.Status = OrderStatus.Paid;
        //    order.Email = customerEmail;
        //    order.Total = total / 100;

        //    _context.SaveChanges();

        //    return View();
        //}

        public IActionResult CheckoutFailed()
        {
            return View();
        }
    }
}
