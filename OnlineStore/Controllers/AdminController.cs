using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Domain;

namespace OnlineStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateCategory() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category) 
        {
            _context.Entry(category).State = EntityState.Added;
            _context.SaveChanges();
            return View();
        }
    }
}
