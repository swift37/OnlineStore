using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Constants;
using OnlineStore.MVC.Models.Event;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Areas.Admin.Controllers
{
    [Area(AreaNames.Admin)]
    [Authorize(Roles = Roles.EmployeeOrHigher)]
    public class EventsController : Controller
    {
        private readonly IEventsService _eventsService;

        public EventsController(IEventsService eventsService) => _eventsService = eventsService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _eventsService.GetAll();

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _eventsService.Get(id);

            if (!response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        public async Task<IActionResult> Exist(int id)
        {
            var response = await _eventsService.Exist(id);

            if (!response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        public IActionResult Create()
        {
            var model = new CreateEventViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        public async Task<IActionResult> Create(CreateEventViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _eventsService.Create(model);

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
            var response = await _eventsService.Get(id);

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpPost]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        public async Task<IActionResult> Update(EventViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _eventsService.Update(model);

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
            var response = await _eventsService.Delete(id);

            if (response.Success) return RedirectToAction("GetAll");

            return StatusCode(response.Status);
        }
    }
}
