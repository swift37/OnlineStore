using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Attributes;
using OnlineStore.MVC.Models;
using OnlineStore.MVC.Models.Review;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IReviewsService _reviewsService;
        private readonly IOrdersService _ordersService;

        public AccountController(
            IAuthService authService, 
            IReviewsService reviewsService, 
            IOrdersService ordersService) =>
            (_authService, _reviewsService, _ordersService) = 
            (authService, reviewsService, ordersService);

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
                return RedirectToAction("Refresh", "Auth", new { redirectUrl = "~/account/settings" });

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
        public async Task<IActionResult> Reviews()
        {
            var reviewsResponse = await _reviewsService.GetUserReviews();
            if (!reviewsResponse.Success)
                return StatusCode(reviewsResponse.Status);

            var ordersResponse = await _ordersService.GetUserOrdersAwaitingReview();
            if (!ordersResponse.Success)
                return StatusCode(ordersResponse.Status);

            var model = new ReviewsPageViewModel
            {
                Reviews = reviewsResponse.Data,
                OrdersAwaitingReview = ordersResponse.Data
            };

            return View(model);
        }

        [HttpGet("account/reviews/reload-list")]
        public IActionResult UpdateUserReviews() => 
            ViewComponent("UserReviewsList");

        [HttpGet("account/reviews/reload-awaiting-list")]
        public IActionResult UpdateOrdersAwaitingReview() => 
            ViewComponent("OrdersAwaitingReviewList");

        [HttpPost("account/reviews/create-review")]
        [ValidateAjax]
        public async Task<IActionResult> CreateReview(CreateReviewViewModel model)
        {
            var response = await _reviewsService.Create(model);

            if (response.Success) return Ok();

            if (response.Status == 400 && response.ValidationErrors.Any())
                return BadRequest(new { errors = response.ValidationErrors });

            return StatusCode(response.Status, new { errors = new[] { $"An error occurred. Status code: {response.Status}" } });
        }

        [HttpPut("account/reviews/update-review")]
        [Authorize]
        [ValidateAjax]
        public async Task<IActionResult> UpdateReview(ReviewViewModel model)
        {
            var response = await _reviewsService.Update(model);

            if (response.Success) return Ok();

            if (response.Status == 400 && response.ValidationErrors.Any())
                return BadRequest(new { errors = response.ValidationErrors });

            return StatusCode(response.Status, new { errors = new[] { $"An error occurred. Status code: {response.Status}" } });
        }

        [HttpGet]
        public async Task<IActionResult> Orders()
        {
            var response = await _ordersService.GetUserOrders();

            if (!response.Success) return StatusCode(response.Status);

            var orders = response.Data
                .OrderByDescending(g => g.CreatingDate)
                .GroupBy(o => o.CreatingDate.ToString("MMMM yyyy"))
                .AsEnumerable();

            return View(orders);
        }

        [HttpGet("account/order-details/{orderNumber}")]
        public async Task<IActionResult> OrderDetails(string orderNumber)
        {
            var response = await _ordersService.Get(orderNumber);

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }
    }
}
