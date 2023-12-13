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

        //[HttpPost]
        //public async Task<IActionResult> Subscribe(CreateSubscriberViewModel model)
        //{
        //    //if (string.IsNullOrWhiteSpace(email) || !email.Contains('@') || !email.Contains('.'))
        //    //    return Json(new { error = true, message = "Invalid email address." });

        //    var response = await _subscribersService.Create(model);

        //    if (response.Success) return Ok();

        //    if (response.Status == 400 && response.ValidationErrors.Count() > 0)
        //    {
        //        foreach (var error in response.ValidationErrors)
        //            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

        //        return View(model);
        //    }

        //    return StatusCode(response.Status);
        //}

        [HttpPost]
        [ValidateAjax]
        public async Task<JsonResult> Subscribe(CreateSubscriberViewModel model)
        {
            var response = await _subscribersService.Create(model);

            if (response.Success) return Json(new { success = true });

            if (response.Status == 400 && response.ValidationErrors.Count() > 0)
                return Json(new 
                { 
                    success = false, 
                    errors = response.ValidationErrors
                        .Select(error => error.ErrorMessage)
                        .ToArray()
                });

            return Json(new { success = false, errors = new[] { $"An error occurred. Status code: {response.Status}" } });
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
