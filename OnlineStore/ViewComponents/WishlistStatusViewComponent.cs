using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;

namespace OnlineStore.ViewComponents
{
    public class WishlistStatusViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public WishlistStatusViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var wishlist = await _context.Wishlists
                .Include(c => c.Products)
                .FirstOrDefaultAsync();

            ViewBag.WishlistItems = wishlist?.TotalQuantity ?? 0;

            return View();
        }
    }
}
