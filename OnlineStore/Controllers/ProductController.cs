using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Domain;

namespace OnlineStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult ViewProduct(int? id)
        {
            if (id is null) return RedirectToAction("Index", "Home");
            return View(_context.Products.SingleOrDefault(p => p.Id == id.Value));
        }

        public IActionResult AddToCart(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product is null) return RedirectToAction("NotFound", "Error");

            var cart = _context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.Status == CartStatus.Active);
            if (cart is null)
            {
                cart = new Cart();
                _context.Carts.Add(cart);
            }

            var item = cart.CartItems?.FirstOrDefault(i => i.Id == product.Id);
            if (item is null) cart.CartItems?.Add(new CartItem { Product = product, Quantity = 1 });
            else item.Quantity++;

            if (product.UnitsInStock < item?.Quantity)
                return Json(new { error = true, message = $"You can`t buy more than {product.UnitsInStock} pcs." });

            return View();
        }
    }
}
