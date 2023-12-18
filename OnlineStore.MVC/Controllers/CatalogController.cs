using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Models.Enums;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductsService _productsService;

        public CatalogController(IProductsService productsService) => 
            _productsService = productsService;

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
    }
}
