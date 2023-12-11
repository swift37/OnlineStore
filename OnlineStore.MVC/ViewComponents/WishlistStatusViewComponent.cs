using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.ViewComponents
{
    public class WishlistStatusViewComponent : ViewComponent
    {
        private readonly IWishlistsService _wishlistsService;

        public WishlistStatusViewComponent(IWishlistsService wishlistsService)
        {
            _wishlistsService = wishlistsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _wishlistsService.GetUserWishlist();
            var wishlist = response.Data;

            ViewBag.WishlistItems = wishlist.Products.Count;

            return View();
        }
    }
}
