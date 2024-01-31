using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Models;
using System.Diagnostics;

namespace OnlineStore.MVC.Controllers
{
    public class ErrorController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        [Route("error/status/{statusCode:int}")]
        public IActionResult Status(int statusCode)
        {
            Response.Clear();
            Response.StatusCode = statusCode;
            switch (statusCode)
            {
                case 401:
                    return User.Identity?.IsAuthenticated is true ? 
                        RedirectToAction("Refresh", "Auth") : RedirectToAction("Login", "Auth");
                case 403:
                    return View("Forbidden");
                case 404:
                    return View("NotFound");
                case 422:
                    return View("UnprocessableEntity");
                case 500:
                    return View("InternalServerError");
                default:
                    return View("GenericError");
            }
        }
    }
}
