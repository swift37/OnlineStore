using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;

namespace OnlineStore.ViewComponents
{
    public class CartStatusViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CartStatusViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.Status == Domain.CartStatus.Active);

            ViewBag.CartItems = cart?.TotalQuantity ?? 0;

            return View();
        }
    }
}
