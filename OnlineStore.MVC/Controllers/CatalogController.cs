using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Attributes;
using OnlineStore.MVC.Models;
using OnlineStore.MVC.Models.Enums;
using OnlineStore.MVC.Models.Review;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly IReviewsService _reviewsService;
        private readonly IFilterGroupsService _filterGroupsService;

        public CatalogController(
            IProductsService productsService, 
            IReviewsService reviewsService,
            IFilterGroupsService filterGroupsService) => 
            (_productsService, _reviewsService, _filterGroupsService) = (productsService, reviewsService, filterGroupsService);

        [HttpGet]
        public async Task<IActionResult> Index(
            int categoryId, 
            int page = 1, 
            int itemsPerPage = 15,
            int minPrice = 0,
            int maxPrice = int.MaxValue,
            SortParameter sortBy = SortParameter.Default)
        {
            var options = new ProductsFilteringOptions
            {
                CategoryId = categoryId,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                PageNumber = page,
                ItemsPerPage = itemsPerPage,
                SortBy = sortBy
            };
           
            var specIds = HttpContext.Request.Query["filters"];
            foreach (var stringId in specIds)
            {
                if (int.TryParse(stringId?.Split(';')[0][4..], out var specTypeId) &&
                    int.TryParse(stringId?.Split(';')[1][3..], out var specId))
                    if (!options.SpecificationIds.TryAdd(specTypeId, new List<int> { specId }))
                        options.SpecificationIds[specTypeId].Add(specId);
            }

            var productsServiceResponse = await _productsService.GetFilteredProducts(options);
            if (!productsServiceResponse.Success) return StatusCode(productsServiceResponse.Status);

            var filterGroupsServiceResponse = await _filterGroupsService.GetCategoryFiltersGroup(categoryId);
            if (!filterGroupsServiceResponse.Success) return StatusCode(productsServiceResponse.Status);

            var model = new CatalogViewModel
            {
                FiltersGroup = filterGroupsServiceResponse.Data,
                AppliedFilterIds = options.SpecificationIds.Values.SelectMany(v => v).ToList(),
                ProductsPage = productsServiceResponse.Data,
            };

            return View(model);
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
