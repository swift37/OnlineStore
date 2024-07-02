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
            ViewBag.UserAuthenticated = false;

            if (!User.Identity?.IsAuthenticated is true) 
                return View();
            else
                ViewBag.UserAuthenticated = true;

            var response = await _wishlistsService.GetUserWishlist();
            var wishlist = response.Data;

            if (response.Success) 
                ViewBag.WishlistItems = wishlist.ProductsCount;
            else 
                ViewBag.WishlistItems = 0;

            return View();
        }
    }
}
