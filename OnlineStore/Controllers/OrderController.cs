using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Domain;
using OnlineStore.Services;

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

            return RedirectToAction("OrderSuccess");
        }

        public IActionResult Payment()
        {
            return View();
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }
    }
}
