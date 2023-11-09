using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;

namespace OnlineStore.ViewComponents
{
    public class MenuItemsGroupViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public MenuItemsGroupViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menuGroup = await _context.MenuItems
                .Include(i => i.Category)
                .Include(i => i.Categories)
                .ToArrayAsync();

            return View(menuGroup);
        }
    }
}
