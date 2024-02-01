using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Constants;
using OnlineStore.MVC.Models;
using OnlineStore.MVC.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OnlineStore.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) => _authService = authService;

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var response = await _authService.Register(model);

            if (response.Success)
                return RedirectToAction("Login");

            if (response.Status == 400 && response.ValidationErrors.Count() > 0)
            {
                foreach (var error in response.ValidationErrors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(model);
            }

            return StatusCode(response.Status);
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl)
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

                return string.IsNullOrEmpty(model.ReturnUrl) ? 
                    RedirectToAction("Settings", "Account") : LocalRedirect(model.ReturnUrl);
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
        [Authorize]
        public async Task<IActionResult> Refresh(string? redirectUrl)
        {
            var token = Request.Cookies[Authorization.XRefreshToken];
            if (token is null) return RedirectToAction("Logout");

            var request = new Models.RefreshRequest
            {
                UserId = Guid.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Sub) ?? string.Empty),
                RefreshToken = token
            };

            var response = await _authService.Refresh(request);
            if (!response.Success) return RedirectToAction("Logout");

            Response.Cookies.Append(Authorization.XAccessToken, response.Data.AccessToken);
            Response.Cookies.Append(Authorization.XRefreshToken, response.Data.RefreshToken);

            return string.IsNullOrEmpty(redirectUrl) ? 
                RedirectToAction("Index", "Home") : LocalRedirect(redirectUrl);
        }

        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(Guid userId, string token)
        {
            var response = await _authService.ConfirmEmail(userId, token);
            if (response.Success)
                return RedirectToAction();

            return StatusCode(response.Status);
        }

        [HttpGet]
        public IActionResult ResetPasswordRequest() => 
            View(new ResetPasswordRequestViewModel());

        [HttpPost]
        public async Task<IActionResult> ResetPasswordRequest(ResetPasswordRequestViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _authService.ResetPasswordRequest(model);
            if (response.Success) 
                return RedirectToAction();

            if (response.Status == 404)
            {
                ModelState.AddModelError(nameof(model.UsernameOrEmail), "Such a user is not found.");
                return View(model);
            }

            return StatusCode(response.Status);
        }

        [HttpGet]
        public IActionResult ResetPassword(Guid userId, string token)
        {
            var model = new ResetPasswordViewModel 
            { 
                UserId = userId, 
                Token = token 
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _authService.ResetPassword(model);
            if (response.Success)
                return RedirectToAction();

            if (response.Status == 400 && response.ValidationErrors.Count() > 0)
            {
                foreach (var error in response.ValidationErrors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(model);
            }

            return StatusCode(response.Status);
        }
    }
}
