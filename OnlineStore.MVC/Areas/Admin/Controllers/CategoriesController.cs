using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Constants;
using OnlineStore.MVC.Models.Category;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Areas.Admin.Controllers
{
    [Area(AreaNames.Admin)]
    [Authorize(Roles = Roles.EmployeeOrHigher)]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService) =>
            _categoriesService = categoriesService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoriesService.GetAll();

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _categoriesService.Get(id);

            if (!response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        public async Task<IActionResult> Exist(int id)
        {
            var response = await _categoriesService.Exist(id);

            if (!response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateCategoryViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        public async Task<IActionResult> Create(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _categoriesService.Create(model);

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
            var response = await _categoriesService.Get(id);

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpPost]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        public async Task<IActionResult> Update(CategoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _categoriesService.Update(model);

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
            var response = await _categoriesService.Delete(id);

            if (response.Success) return RedirectToAction("GetAll");

            return StatusCode(response.Status);
        }
    }
}
