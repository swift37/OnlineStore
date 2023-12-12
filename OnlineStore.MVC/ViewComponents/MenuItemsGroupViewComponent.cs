using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.ViewComponents
{
    public class MenuItemsGroupViewComponent : ViewComponent
    {
        private readonly IMenuItemsService _menuItemsService;

        public MenuItemsGroupViewComponent(IMenuItemsService menuItemsService) => 
            _menuItemsService = menuItemsService;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _menuItemsService.GetAll();
            var model = response.Data;

            return View(model);
        }
    }
}
