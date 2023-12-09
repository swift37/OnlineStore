using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Constants;
using OnlineStore.MVC.Models.Coupon;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Controllers
{
    [Authorize(Roles = Roles.EmployeeOrHigher)]
    public class CouponsController : Controller
    {
        private readonly ICouponsService _couponsService;

        public CouponsController(ICouponsService couponsService) => _couponsService = couponsService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _couponsService.GetAll();

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _couponsService.Get(id);

            if (!response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        public async Task<IActionResult> Exist(int id)
        {
            var response = await _couponsService.Exist(id);

            if (!response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        public IActionResult Create()
        {
            var model = new CreateCouponViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        public async Task<IActionResult> Create(CreateCouponViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _couponsService.Create(model);

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
            var response = await _couponsService.Get(id);

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpPost]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        public async Task<IActionResult> Update(CouponViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _couponsService.Update(model);

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
            var response = await _couponsService.Delete(id);

            if (response.Success) return RedirectToAction("GetAll");

            return StatusCode(response.Status);
        }
    }
}
