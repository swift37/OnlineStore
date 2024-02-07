using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Extensions;
using OnlineStore.MVC.Models;
using OnlineStore.MVC.Models.Enums;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly IFilterGroupsService _filterGroupsService;

        public CatalogController(
            IProductsService productsService, 
            IFilterGroupsService filterGroupsService) => 
            (_productsService, _filterGroupsService) = (productsService, filterGroupsService);

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
            options.SpecificationIds = specIds.GetAppliedFilters();

            var productsServiceResponse = await _productsService.GetFilteredProducts(options);
            if (!productsServiceResponse.Success) return StatusCode(productsServiceResponse.Status);

            var filterGroupsServiceResponse = await _filterGroupsService.GetCategoryFiltersGroup(categoryId);
            if (!filterGroupsServiceResponse.Success) return StatusCode(productsServiceResponse.Status);

            var model = new CatalogViewModel
            {
                FiltersGroup = filterGroupsServiceResponse.Data,
                AppliedFilterIds = specIds.GetAppliedFilterIds(),
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
    }
}
