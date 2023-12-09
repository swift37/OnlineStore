using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Constants;
using OnlineStore.MVC.Models.Subscriber;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Controllers
{
    public class SubscribersController : Controller
    {
        private readonly ISubscribersService _subscribersService;

        public SubscribersController(ISubscribersService subscribersService) => 
            _subscribersService = subscribersService;

        [HttpGet]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _subscribersService.GetAll();

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _subscribersService.Get(id);

            if (!response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        public async Task<IActionResult> Exist(int id)
        {
            var response = await _subscribersService.Exist(id);

            if (!response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateSubscriberViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSubscriberViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _subscribersService.Create(model);

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
            var response = await _subscribersService.Get(id);

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpPost]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        public async Task<IActionResult> Update(SubscriberViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _subscribersService.Update(model);

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
            var response = await _subscribersService.Delete(id);

            if (response.Success) return RedirectToAction("GetAll");

            return StatusCode(response.Status);
        }
    }
}
