using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Domain;
using OnlineStore.Models.ViewModels;

namespace OnlineStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private int _pageSize = 25;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("products")]
        public IActionResult GetProductsBySubCategory(int? subCategoryId, int page = 1)
        {
            if (subCategoryId is null) return RedirectToAction("Index", "Home");

            var subCategory = _context.SubCategories
                .Include(sc => sc.Category)
                .SingleOrDefault(sc => sc.Id == subCategoryId);

            if (subCategory is null) return RedirectToAction("Error", "NotFound");

            var pagesCount = (_context.Products.Count() + _pageSize - 1) / _pageSize;
            var productsList = _context.Products
                .Skip((page - 1) * _pageSize)
                .Take(_pageSize)
                .Include(p => p.SubCategory);
            var model = new ProductsCollectionViewModel(
                productsList, 
                subCategory.Category, 
                subCategory, 
                page, 
                pagesCount
                );

            return View(model);
        }

        [Route("product")]
        public IActionResult ViewProduct(int? id)
        {
            if (id is null) return RedirectToAction("Index", "Home");
            return View(_context.Products.Include(p => p.Specification).SingleOrDefault(p => p.Id == id));
        }

        public IActionResult AddToCart(int productId, int qty = 1)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == productId);
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

        public IActionResult UpdateMiniCart()
        {
            return ViewComponent("MiniCart");
        }

        public async Task<IActionResult> AddToWishlist(int productId) 
        {
            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == productId);
            if (product is null) return RedirectToAction("NotFound", "Error");

            var wishlist = await _context.Wishlists
                .Include(c => c.Products)
                .FirstOrDefaultAsync(/*c => c.User.Id == User.Identity.GetUserId() &&*/);

            if (wishlist is null)
            {
                wishlist = new Wishlist();
                await _context.Wishlists.AddAsync(wishlist);
            }

            if (wishlist.Products.Any(p => p.Id == productId)) 
                return Json(new { error = true, message = "The product is already on your wishlist." });

            var item = wishlist.Products.FirstOrDefault(p => p.Id == product.Id);
            if (item is null) wishlist.Products?.Add(product);

            await _context.SaveChangesAsync();
            return Json(new { error = false });
        }

        public async Task<IActionResult> RemoveFromWishlist(int productId)
        {
            var wishlist = await _context.Wishlists
                .Include(c => c.Products)
                .FirstOrDefaultAsync();

            var item = wishlist?.Products.SingleOrDefault(p => p.Id == productId);

            if (wishlist is null || item is null) return Json(new { removeSuccess = false });

            wishlist.Products.Remove(item);
            _context.Entry(wishlist).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Json(new { removeSuccess = true });
        }

        [Route("wishlist")]
        public IActionResult ViewWishlist()
        {
            var wishlist = _context.Wishlists
                .Include(c => c.Products)
                .FirstOrDefault();

            return View(wishlist);
        }

        public IActionResult AddAllWishlistToCart(int wishlistId)
        {
            var wishlist = _context.Wishlists.Include(w => w.Products).FirstOrDefault(w => w.Id == wishlistId);

            if (wishlist is null) return RedirectToAction("NotFound", "Error");

            foreach (var item in wishlist.Products) 
                AddToCart(item.Id);

            _context.Wishlists.Remove(wishlist);
            _context.SaveChanges();

            return Redirect("/cart");
        } 
    }
}
