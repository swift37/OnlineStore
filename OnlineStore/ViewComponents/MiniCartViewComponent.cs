using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.Context;

namespace OnlineStore.ViewComponents
{
    public class MiniCartViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public MiniCartViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<IViewComponentResult> InvokeAsync()
        //{
        //    var cart = await _context.Carts
        //        .Include(c => c.CartItems)
        //        .ThenInclude(i => i.Product)
        //        .FirstOrDefaultAsync(c => c.Status == Domain.CartStatus.Active);

        //    return View(cart);
        //}
    }
}
