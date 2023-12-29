using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Attributes;
using OnlineStore.MVC.Models.Enums;
using OnlineStore.MVC.Models.Review;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly IReviewsService _reviewsService;

        public CatalogController(IProductsService productsService, IReviewsService reviewsService) => 
            (_productsService, _reviewsService) = (productsService, reviewsService);

        [HttpGet]
        public async Task<IActionResult> Index(
            int categoryId, 
            int page = 1, 
            int itemsPerPage = 15, 
            SortParameters sortBy = SortParameters.Default)
        {
            var response = await _productsService
                .GetProductsByCategory(categoryId, page, itemsPerPage, sortBy);

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        public async Task<IActionResult> Product(int id)
        {
            var response = await _productsService.Get(id);

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpPost]
        [Authorize]
        [ValidateAjax]
        public async Task<IActionResult> CreateReview(CreateReviewViewModel model)
        {
            var response = await _reviewsService.Create(model);

            if (response.Success) return Json(new { success = true });

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
