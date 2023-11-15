using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.Context;
using OnlineStore.Domain;
using OnlineStore.Models.ViewModels;

namespace OnlineStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("products", Name = "products")]
        public IActionResult GetProductsBySubCategory(
            int catId, 
            int page = 1, 
            int itemsPerPage = 15, 
            SortParameter sortBy = SortParameter.Default)
        {
            if (itemsPerPage > 30 || itemsPerPage < 15) itemsPerPage = 15;

            var category = _context.Categories
                .Include(c => c.Parent)
                .SingleOrDefault(c => c.Id == catId);

            if (category is null) return RedirectToAction("Error", "NotFound");

            var pagesCount = (_context.Products.Count() + itemsPerPage - 1) / itemsPerPage;
            var productsList = _context.Products
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Include(p => p.Category);

            var productsSortedList = SortProducts(productsList, sortBy);

            var model = new ProductsCollectionViewModel(
                productsSortedList,
                category, 
                page, 
                pagesCount,
                itemsPerPage);

            return View(model);
        }

        private IEnumerable<Product> SortProducts(IEnumerable<Product> products, SortParameter sortBy)
        {
            switch (sortBy) 
            {
                default:
                    return products;
                case SortParameter.RatingDescending:
                    return products.OrderByDescending(p => p.Rating);
                case SortParameter.PriceAscending:
                    return products.OrderBy(p => p.UnitPrice);
                case SortParameter.PriceDescending:
                    return products.OrderByDescending(p => p.UnitPrice);
            }
        }

        [Route("product")]
        public IActionResult ViewProduct(int? id)
        {
            if (id is null) return RedirectToAction("Index", "Home");
            return View(_context.Products.Include(p => p.Specifications).SingleOrDefault(p => p.Id == id));
        }

        //[Route("cart")]
        //public IActionResult ViewCart()
        //{
        //    var cart = _context.Carts
        //        .Include(c => c.CartItems)
        //        .ThenInclude(i => i.Product)
        //        .FirstOrDefault(c => c.Status == CartStatus.Active);

        //    return View(cart);
        //}

        //[Route("wishlist")]
        //public IActionResult ViewWishlist()
        //{
        //    var wishlist = _context.Wishlists
        //        .Include(c => c.Products)
        //        .FirstOrDefault();

        //    return View(wishlist);
        //}

        //public IActionResult AddToCart(int productId, int qty = 1)
        //{
        //    var product = _context.Products.SingleOrDefault(p => p.Id == productId);
        //    if (product is null) return RedirectToAction("NotFound", "Error");

        //    var cart = _context.Carts
        //        .Include(c => c.CartItems)
        //        .ThenInclude(i => i.Product)
        //        .FirstOrDefault(c => /*c.User.Id == User.Identity.GetUserId() &&*/ c.Status == CartStatus.Active);

        //    if (cart is null)
        //    {
        //        cart = new Cart();
        //        _context.Carts.Add(cart);
        //    }

        //    var item = cart.CartItems?.FirstOrDefault(i => i.Product?.Id == product.Id);
        //    if (item is null) cart.CartItems?.Add(new CartItem { Product = product, Quantity = qty });
        //    else item.Quantity += qty;

        //    if (product.UnitsInStock < item?.Quantity)
        //        return Json(new { error = true, message = $"You can`t buy more than {product.UnitsInStock} pcs." });

        //    _context.SaveChanges();
        //    return Json(new { error = false });
        //}

        //public async Task<IActionResult> UpdateCart(int productId, int qty)
        //{
        //    var cart = await _context.Carts
        //        .Include(c => c.CartItems)
        //        .ThenInclude(i => i.Product)
        //        .FirstOrDefaultAsync(c => c.Status == CartStatus.Active);

        //    var cartItem = cart?.CartItems.FirstOrDefault(i => i.Product?.Id == productId);

        //    if (cart is null || cartItem is null) return Json(new { error = true, message = "Error occurred." });

        //    cartItem.Quantity = qty;
        //    await _context.SaveChangesAsync();
        //    return Json(new
        //    {
        //        error = false,
        //        SubtotalPrice = cart.SubtotalPrice,
        //        TotalPrice = cart.TotalPrice,
        //        TotalDiscount = cart.TotalDiscount,
        //        TotalQuantity = cart.TotalQuantity,
        //        LinePrice = cartItem.Price
        //    });
        //}

        //public async Task<IActionResult> RemoveFromCart(int productId)
        //{
        //    var cart = await _context.Carts
        //        .Include(c => c.CartItems)
        //        .FirstOrDefaultAsync(c => c.Status == CartStatus.Active);

        //    var cartItem = cart?.CartItems.SingleOrDefault(p => p.ProductId == productId);

        //    if (cart is null || cartItem is null) return Json(new { removeSuccess = false });

        //    cart.CartItems.Remove(cartItem);
        //    _context.Entry(cart).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //    return Json(new { removeSuccess = true });
        //}

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

        //public IActionResult AddAllWishlistToCart(int wishlistId)
        //{
        //    var wishlist = _context.Wishlists.Include(w => w.Products).FirstOrDefault(w => w.Id == wishlistId);

        //    if (wishlist is null) return RedirectToAction("NotFound", "Error");

        //    foreach (var item in wishlist.Products) 
        //        AddToCart(item.Id);

        //    _context.Wishlists.Remove(wishlist);
        //    _context.SaveChanges();

        //    return Redirect("/cart");
        //} 
    }

    public enum SortParameter
    {
        Default,
        RatingDescending,
        PriceAscending,
        PriceDescending
    }
}
