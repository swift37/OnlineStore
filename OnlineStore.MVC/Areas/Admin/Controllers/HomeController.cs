using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Constants;

namespace OnlineStore.MVC.Areas.Admin.Controllers
{
    [Area(AreaNames.Admin)]
    [Authorize(Roles = Roles.EmployeeOrHigher)]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
