using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Constants;
using OnlineStore.MVC.Models.Order;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Areas.Admin.Controllers
{
    [Area(AreaNames.Admin)]
    [Authorize(Roles = Roles.EmployeeOrHigher)]
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService) => _ordersService = ordersService;

        [HttpGet]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _ordersService.GetAll();

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _ordersService.Get(id);

            if (!response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        public async Task<IActionResult> Exist(int id)
        {
            var response = await _ordersService.Exist(id);

            if (!response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateOrderViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _ordersService.Create(model);

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
            var response = await _ordersService.Get(id);

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpPost]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        public async Task<IActionResult> Update(OrderViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _ordersService.Update(model);

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
            var response = await _ordersService.Delete(id);

            if (response.Success) return RedirectToAction("GetAll");

            return StatusCode(response.Status);
        }

        [HttpGet]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        public async Task<IActionResult> GetUserOrders(Guid userId)
        {
            var response = await _ordersService.GetUserOrders(userId);

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserOrders()
        {
            var response = await _ordersService.GetUserOrders();

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserOrder(int id)
        {
            var response = await _ordersService.GetUserOrder(id);

            if (response.Success) return View(response.Data);

            return StatusCode(response.Status);
        }
    }
}
