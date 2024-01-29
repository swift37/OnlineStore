using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Attributes;
using OnlineStore.MVC.Models.Wishlist;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IWishlistsService _wishlistsService;

        public WishlistController(IWishlistsService wishlistsService) =>
            _wishlistsService = wishlistsService;

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
        public async Task<IActionResult> Add(CreateWishlistItemViewModel model)
        {
            var response = await _wishlistsService.AddItem(model);
            if (response.Success)
                return Json(new { success = true });

            if (response.Status == 400 && response.ValidationErrors.Count() > 0)
                return Json(new
                {
                    success = false,
                    errors = response.ValidationErrors
                        .Select(error => error.ErrorMessage)
                        .ToArray()
                });

            return Json(new { success = false, errors = new[] { $"An error occurred. Status code: {response.Status}" } });
        }

        [HttpPut]
        [Authorize]
        [ValidateAjax]
        public async Task<IActionResult> Update(WishlistItemViewModel model)
        {
            var response = await _wishlistsService.UpdateItem(model);
            if (response.Success)
                return Json(new { success = true });

            if (response.Status == 400 && response.ValidationErrors.Count() > 0)
                return Json(new
                {
                    success = false,
                    errors = response.ValidationErrors
                        .Select(error => error.ErrorMessage)
                        .ToArray()
                });

            return Json(new { success = false, errors = new[] { $"An error occurred. Status code: {response.Status}" } });
        }

        [HttpDelete]
        [Authorize]
        [ValidateAjax]
        public async Task<IActionResult> Remove(int itemId)
        {          
            var response = await _wishlistsService.RemoveItem(itemId);
            if (response.Success)
                return Json(new { success = true });

            if (response.Status == 400 && response.ValidationErrors.Count() > 0)
                return Json(new
                {
                    success = false,
                    errors = response.ValidationErrors
                        .Select(error => error.ErrorMessage)
                        .ToArray()
                });

            return Json(new { success = false, errors = new[] { $"An error occurred. Status code: {response.Status}" } });
        }

        [HttpDelete]
        [Authorize]
        [ValidateAjax]
        public async Task<IActionResult> RemoveRange(IEnumerable<int> itemIds)
        {
            var response = await _wishlistsService.RemoveItems(itemIds);
            if (response.Success)
                return Json(new { success = true });

            if (response.Status == 400 && response.ValidationErrors.Count() > 0)
                return Json(new
                {
                    success = false,
                    errors = response.ValidationErrors
                        .Select(error => error.ErrorMessage)
                        .ToArray()
                });

            return Json(new { success = false, errors = new[] { $"An error occurred. Status code: {response.Status}" } });
        }
    }
}
