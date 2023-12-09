using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Constants;
using OnlineStore.MVC.Models.ContactRequest;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Areas.Admin.Controllers
{
    [Area(AreaNames.Admin)]
    [Authorize(Roles = Roles.EmployeeOrHigher)]
    public class ContactRequestsController : Controller
    {
        private readonly IContactRequestsService _contactRequestsService;

        public ContactRequestsController(IContactRequestsService contactRequestsService) =>
            _contactRequestsService = contactRequestsService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _contactRequestsService.GetAll();

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _contactRequestsService.Get(id);

            if (!response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        public async Task<IActionResult> Exist(int id)
        {
            var response = await _contactRequestsService.Exist(id);

            if (!response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Administrator)]
        public IActionResult Create()
        {
            var model = new CreateContactRequestViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Create(CreateContactRequestViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _contactRequestsService.Create(model);

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
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _contactRequestsService.Get(id);

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpPost]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        public async Task<IActionResult> Update(ContactRequestViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _contactRequestsService.Update(model);

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
            var response = await _contactRequestsService.Delete(id);

            if (response.Success) return RedirectToAction("GetAll");

            return StatusCode(response.Status);
        }
    }
}
