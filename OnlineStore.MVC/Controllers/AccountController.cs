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

        [HttpPost("account/settings/user/update")]
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

        [HttpGet("account/credentials/change/email")]
        public IActionResult ChangeEmail() => View(new ChangeEmailViewModel());

        [HttpPost("account/credentials/change/email")]
        public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _authService.ChangeEmail(model);
            if (response.Success)
                return RedirectToRoute("account/credentials/change/confirmation");

            if (response.Status == 400 && response.ValidationErrors.Count() > 0)
            {
                foreach (var error in response.ValidationErrors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(model);
            }

            return StatusCode(response.Status);
        }

        [HttpGet("account/credentials/change/email/confirmation")]
        public async Task<IActionResult> ChangeEmailConfirmation(Guid userId, string newEmail, string token)
        {
            var response = await _authService.ConfirmEmailChanging(userId, newEmail, token);
            if (response.Success)
                return RedirectToRoute("account/credentials/change/success");

            return StatusCode(response.Status);
        }

        [HttpGet("account/credentials/change/password")]
        public IActionResult ChangePassword() => View(new ChangePasswordViewModel());

        [HttpPost("account/credentials/change/password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _authService.ChangePassword(model);
            if (response.Success)
                return RedirectToRoute("account/credentials/change/success");

            if (response.Status == 400 && response.ValidationErrors.Count() > 0)
            {
                foreach (var error in response.ValidationErrors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(model);
            }

            return StatusCode(response.Status);
        }

        [HttpGet("account/credentials/change/confirmation")]
        public IActionResult ChangeCredentialsConfirmation() => View();

        [HttpGet("account/credentials/change/success")]
        public IActionResult ChangeCredentialsSuccess() => View();

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
