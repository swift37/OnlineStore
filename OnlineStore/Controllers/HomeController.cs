using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models.ViewModels;
using System.Diagnostics;

namespace OnlineStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private int _pageSize = 1;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Catalog(int page = 1)
        {
            var pagesCount = (_context.Products.Count() + _pageSize - 1) / _pageSize;
            var productsList = _context.Products
                .Skip((page - 1) * _pageSize)
                .Take(_pageSize)
                .Include(p => p.SubCategory);
            var model = new ProductsListViewModel(productsList, page, pagesCount);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}