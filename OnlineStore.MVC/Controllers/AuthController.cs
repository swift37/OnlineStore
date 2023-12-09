using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Constants;
using OnlineStore.MVC.Services.Interfaces;
using OnlineStore.WebAPI.Models;

namespace OnlineStore.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) => _authService = authService;

        [HttpGet]
        public IActionResult Register(string? returnUrl)
        {
            var model = new RegisterViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var response = await _authService.Register(model);

            if (response.Success)
            {
                var url = model.ReturnUrl ??= Url.Content("~/");
                return LocalRedirect(model.ReturnUrl);
            }

            if (response.Status == 400 && response.ValidationErrors.Count() > 0)
            {
                foreach (var error in response.ValidationErrors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(model);
            }

            return StatusCode(response.Status);
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _authService.Login(model);

            if (response.Success)
            {
                Response.Cookies.Append(Authorization.XAccessToken, response.Data.AccessToken);
                Response.Cookies.Append(Authorization.XRefreshToken, response.Data.RefreshToken);

                var url = model.ReturnUrl ??= Url.Content("~/");
                return LocalRedirect(model.ReturnUrl);
            }

            if (response.Status == 400 && response.ValidationErrors.Count() > 0)
            {
                foreach (var error in response.ValidationErrors) 
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(model);
            }

            return StatusCode(response.Status);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Refresh()
        {
            var token = Request.Cookies[Authorization.XRefreshToken];
            if (token is null) return RedirectToAction("Logout");

            var response = await _authService.Refresh(token);

            if (response.Success)
            {
                Response.Cookies.Append(Authorization.XAccessToken, response.Data.AccessToken);
                Response.Cookies.Append(Authorization.XRefreshToken, response.Data.RefreshToken);

                return LocalRedirect(Url.Content("~/"));
            }

            return StatusCode(response.Status);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var response = await _authService.Logout();

            if (response.Success)
            {
                Response.Cookies.Delete(Authorization.XAccessToken);
                Response.Cookies.Delete(Authorization.XRefreshToken);
            }

            return RedirectToAction("Login");
        }
    }
}
