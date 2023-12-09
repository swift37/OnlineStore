using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Constants;
using OnlineStore.MVC.Models.Enums;
using OnlineStore.MVC.Models.Product;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService) => _productsService = productsService;

        [HttpGet]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productsService.GetAll();

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _productsService.Get(id);

            if (!response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        public async Task<IActionResult> Exist(int id)
        {
            var response = await _productsService.Exist(id);

            if (!response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        public IActionResult Create()
        {
            var model = new CreateProductViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _productsService.Create(model);

            if (response.Success)
                return RedirectToAction("GetAll");

            if (response.Status == 400 && response.ValidationErrors.Count() > 0)
            {
                foreach (var error in response.ValidationErrors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(model);
            }

            return StatusCode(response.Status);
        }

        [HttpGet]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _productsService.Get(id);

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpPost]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        public async Task<IActionResult> Update(ProductViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _productsService.Update(model);

            if (response.Success)
                return RedirectToAction("GetAll");

            if (response.Status == 400 && response.ValidationErrors.Count() > 0)
            {
                foreach (var error in response.ValidationErrors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(model);
            }

            return StatusCode(response.Status);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _productsService.Delete(id);

            if (response.Success) return RedirectToAction("GetAll");

            return StatusCode(response.Status);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductByCategory(
            int categoryId,
            int page = 1,
            int itemsPerPage = 15,
            SortParameters sortBy = SortParameters.Default)
        {
            var response = await _productsService.GetProductsByCategory(categoryId);

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }
    }
}
