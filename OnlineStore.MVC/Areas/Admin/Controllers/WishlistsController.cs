using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Constants;
using OnlineStore.MVC.Models.Wishlist;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Areas.Admin.Controllers
{
    [Area(AreaNames.Admin)]
    [Authorize(Roles = Roles.EmployeeOrHigher)]
    public class WishlistsController : Controller
    {
        private readonly IWishlistsService _wishlistsService;

        public WishlistsController(IWishlistsService wishlistsService) =>
            _wishlistsService = wishlistsService;

        [HttpGet]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _wishlistsService.GetAll();

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _wishlistsService.Get(id);

            if (!response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Exist(int id)
        {
            var response = await _wishlistsService.Exist(id);

            if (!response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateWishlistViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWishlistViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _wishlistsService.Create(model);

            if (response.Success)
                return RedirectToAction("GetAll");

            if (response.Status == 400 && response.ValidationErrors.Count() > 0)
            {
                foreach (var error in response.ValidationErrors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(model);
            }

            return StatusCode(response.Status);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _wishlistsService.Get(id);

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpPost]
        public async Task<IActionResult> Update(WishlistViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _wishlistsService.Update(model);

            if (response.Success)
                return RedirectToAction("GetAll");

            if (response.Status == 400 && response.ValidationErrors.Count() > 0)
            {
                foreach (var error in response.ValidationErrors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(model);
            }

            return StatusCode(response.Status);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _wishlistsService.Delete(id);

            if (response.Success) return RedirectToAction("GetAll");

            return StatusCode(response.Status);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> GetUserWishlist(Guid userId)
        {
            var response = await _wishlistsService.GetUserWishlist(userId);

            if (!response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserWishlist()
        {
            var response = await _wishlistsService.GetUserWishlist();

            if (!response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }
    }
}
