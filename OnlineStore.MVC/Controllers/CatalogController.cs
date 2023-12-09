using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.MVC.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
