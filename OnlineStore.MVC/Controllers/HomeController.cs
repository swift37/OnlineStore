using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Attributes;
using OnlineStore.MVC.Models;
using OnlineStore.MVC.Models.ContactRequest;
using OnlineStore.MVC.Models.Subscriber;
using OnlineStore.MVC.Services.Interfaces;
using System.Diagnostics;

namespace OnlineStore.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISubscribersService _subscribersService;
        private readonly IContactRequestsService _contactRequestsService;

        public HomeController(
            ILogger<HomeController> logger, 
            ISubscribersService subscribersService,
            IContactRequestsService contactRequestsService)
        {
            _logger = logger;
            _subscribersService = subscribersService;
            _contactRequestsService = contactRequestsService;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Contact() => View();

        [HttpGet]
        public IActionResult About() => View();

        [HttpGet]
        public IActionResult Events() => View();

        [HttpPost]
        [ValidateAjax]
        public async Task<IActionResult> Subscribe(CreateSubscriberViewModel model)
        {
            var response = await _subscribersService.Create(model);

            if (response.Success) return Ok();

            if (response.Status == 400 && response.ValidationErrors.Any())
                return BadRequest(new { errors = response.ValidationErrors });

            return StatusCode(response.Status, new { errors = new[] { $"An error occurred. Status code: {response.Status}" } });
        }

        [HttpPost]
        [ValidateAjax]
        public async Task<IActionResult> SendContactRequest(CreateContactRequestViewModel model)
        {
            var response = await _contactRequestsService.Create(model);

            if (response.Success) return Ok();

            if (response.Status == 400 && response.ValidationErrors.Any())
                return BadRequest(new { errors = response.ValidationErrors });

            return StatusCode(response.Status, new { errors = new[] { $"An error occurred. Status code: {response.Status}" } });
        }
    }
}
