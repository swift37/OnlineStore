using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.ViewComponents
{
    public class WishlistButtonViewComponent : ViewComponent
    {
        private readonly IWishlistsService _wishlistsService;

        public WishlistButtonViewComponent(IWishlistsService wishlistsService) => 
            _wishlistsService = wishlistsService;

        public async Task<IViewComponentResult> InvokeAsync(int productId, string? text)
        {
            var response = await _wishlistsService.CheckProductPresence(productId);
            var result = response.Data;

            if (result)
                ViewBag.ItemId = (await _wishlistsService.GetItemId(productId)).Data;
            
            ViewBag.ProductId = productId;
            ViewBag.Text = text;

            return View();
        }
    }
}
