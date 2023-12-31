using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Attributes;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IWishlistsService _wishlistsService;
        private readonly IProductsService _productsService;

        public WishlistController(IWishlistsService wishlistsService, IProductsService productsService) =>
            (_wishlistsService, _productsService) = (wishlistsService, productsService);

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var response = await _wishlistsService.GetUserWishlist();

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpPost]
        [Authorize]
        [ValidateAjax]
        public async Task<IActionResult> Add(int productId)
        {
            var getWishlistReponse = await _wishlistsService.GetUserWishlist();
            if (!getWishlistReponse.Success) 
                return Json(new 
                { 
                    success = false, 
                    errors = new[] { $"An error occurred. Status code: {getWishlistReponse.Status}" } 
                });

            var getProductReponse = await _productsService.Get(productId);
            if (!getProductReponse.Success) 
                return Json(new 
                { 
                    success = false, 
                    errors = new[] { $"An error occurred. Status code: {getProductReponse.Status}" } 
                });

            var wishlist = getWishlistReponse.Data;
            var product = getProductReponse.Data;
            
            if (wishlist.Products.Any(product => product.Id == productId))
                return Json(new 
                { 
                    success = false, 
                    errors = new[] { "Such a product is already on your wish list." } 
                });

            wishlist.Products.Add(product);
            wishlist.LastChangeDate = DateTime.Now;

            var updateWishlistReponse = await _wishlistsService.UpdateProducts(wishlist);
            if (updateWishlistReponse.Success) 
                return Json(new { success = true });

            if (updateWishlistReponse.Status == 400 && updateWishlistReponse.ValidationErrors.Count() > 0)
                return Json(new
                {
                    success = false,
                    errors = updateWishlistReponse.ValidationErrors
                        .Select(error => error.ErrorMessage)
                        .ToArray()
                });

            return Json(new { success = false, errors = new[] { $"An error occurred. Status code: {updateWishlistReponse.Status}" } });
        }

        [HttpPost]
        [Authorize]
        [ValidateAjax]
        public async Task<IActionResult> Remove(int productId)
        {
            var getWishlistReponse = await _wishlistsService.GetUserWishlist();
            if (!getWishlistReponse.Success) 
                return Json(new 
                { 
                    success = false, 
                    errors = new[] { $"An error occurred. Status code: {getWishlistReponse.Status}" } 
                });

            var wishlist = getWishlistReponse.Data;
            var product = wishlist.Products.SingleOrDefault(product => product.Id == productId);

            if (product is null) 
                return Json(new 
                { 
                    success = false, 
                    errors = new[] { "Such a product is not on your wish list." } 
                });

            wishlist.Products.Remove(product);
            wishlist.LastChangeDate = DateTime.Now;
            
            var updateWishlistReponse = await _wishlistsService.UpdateProducts(wishlist);
            if (updateWishlistReponse.Success)
                return Json(new { success = true });

            if (updateWishlistReponse.Status == 400 && updateWishlistReponse.ValidationErrors.Count() > 0)
                return Json(new
                {
                    success = false,
                    errors = updateWishlistReponse.ValidationErrors
                        .Select(error => error.ErrorMessage)
                        .ToArray()
                });

            return Json(new { success = false, errors = new[] { $"An error occurred. Status code: {updateWishlistReponse.Status}" } });
        }
    }
}
