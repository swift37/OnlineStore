using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Models;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService) => _authService = authService;

        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Settings() => View(new UpdateUserViewModel());

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _authService.UpdateUser(model);
            if (response.Success)
                return RedirectToAction("Settings");

            if (response.Status == 400 && response.ValidationErrors.Count() > 0)
            {
                foreach (var error in response.ValidationErrors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(model);
            }

            return StatusCode(response.Status);
        }

        [HttpGet]
        public IActionResult Wishlist()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Reviews()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Orders()
        {
            return View();
        }

        [HttpGet]
        public IActionResult OrderDetails()
        {
            return View();
        }
    }
}
