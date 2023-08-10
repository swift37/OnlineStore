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
            return View(_context.Products.Include(p => p.Specification).SingleOrDefault(p => p.Id == id));
        }

        public IActionResult AddToCart(int id, int qty = 1)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product is null) return RedirectToAction("NotFound", "Error");

            var cart = _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(c => /*c.User.Id == User.Identity.GetUserId() &&*/ c.Status == CartStatus.Active);

            if (cart is null)
            {
                cart = new Cart();
                _context.Carts.Add(cart);
            }

            var item = cart.CartItems?.FirstOrDefault(i => i.Product?.Id == product.Id);
            if (item is null) cart.CartItems?.Add(new CartItem { Product = product, Quantity = qty });
            else item.Quantity += qty;

            if (product.UnitsInStock < item?.Quantity)
                return Json(new { error = true, message = $"You can`t buy more than {product.UnitsInStock} pcs." });

            _context.SaveChanges();
            return Json(new { error = false });
        }
    }
}
