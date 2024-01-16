using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.MVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Settings()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Wishlist()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Reviews()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Orders()
        {
            return View();
        }

        [HttpGet]
        public IActionResult OrderDetails()
        {
            return View();
        }
    }
}
