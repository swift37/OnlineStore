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
        private readonly ISpecificationTypesService _specificationTypesService;

        public CatalogController(
            IProductsService productsService, 
            IFilterGroupsService filterGroupsService,
            ISpecificationTypesService specificationTypesService) => 
            (_productsService, _filterGroupsService, _specificationTypesService) = 
            (productsService, filterGroupsService, specificationTypesService);

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

            options.SpecificationIds = HttpContext.Request.Query["filters"].GetAppliedFilters();

            var productsServiceResponse = await _productsService.GetFilteredProducts(options);
            if (!productsServiceResponse.Success) return StatusCode(productsServiceResponse.Status);

            var filterGroupsServiceResponse = await _filterGroupsService.GetCategoryFiltersGroup(categoryId);
            if (!filterGroupsServiceResponse.Success) return StatusCode(productsServiceResponse.Status);

            var model = new CatalogViewModel
            {
                FiltersGroup = filterGroupsServiceResponse.Data,
                ProductsPage = productsServiceResponse.Data,
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProducts(
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

            options.SpecificationIds = HttpContext.Request.Query["filters"].GetAppliedFilters();

            var response = await _productsService.GetFilteredProducts(options);
            if (!response.Success) return StatusCode(response.Status);

            return ViewComponent("ProductCardsList", new { model = response.Data.Products });
        }

        [HttpGet]
        public async Task<IActionResult> Product(int id)
        {
            var response = await _productsService.Get(id);

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateFilterBlock(int specificationTypeId, bool showAll)
        {
            var response = await _specificationTypesService.Get(specificationTypeId);

            if (response.Success) return ViewComponent("FilterBlock", new { model = response.Data, showAll });

            return StatusCode(response.Status);
        }
    }
}
