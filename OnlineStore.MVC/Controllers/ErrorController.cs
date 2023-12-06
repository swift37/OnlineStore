using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.MVC.Controllers
{
    public class ErrorController : Controller
    {
        [Route("error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            Response.Clear();
            Response.StatusCode = statusCode;
            switch (statusCode)
            {
                case 401:
                    return View("Unauthorized");
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
