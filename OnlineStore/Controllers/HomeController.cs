using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Domain;
using OnlineStore.Models.ViewModels;
using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;

namespace OnlineStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private int _pageSize = 25;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Events()
        {
            return View();
        }

        public async Task<IActionResult> SubscribeToNewsletter(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@')) 
                return Json(new { error = true, message = "Invalid email address." });
            if (_context.Subscribers.Any(s => s.Email == email))
                return Json(new { error = true, message = "This email address has already registered." });

            await _context.Subscribers.AddAsync(new Subscriber { Email = email, SubscribeDate = DateTime.Now });
            await _context.SaveChangesAsync();

            return Json(new { error = false });
        }

        public async Task<IActionResult> SendContactRequest(string name, string email, string message)
        {
            if (string.IsNullOrWhiteSpace(name) || 
                string.IsNullOrWhiteSpace(email) || 
                !email.Contains('@') ||
                string.IsNullOrWhiteSpace(message))
                return Json(new { error = true, message = "Invalid data." });

            await _context.ContactRequests.AddAsync(
                new ContactRequest { Name = name, Email = email, Message = message, CreationDate = DateTime.Now });
            await _context.SaveChangesAsync();

            return Json(new { error = false });
        }

        public IActionResult Catalog(int page = 1)
        {
            var pagesCount = (_context.Products.Count() + _pageSize - 1) / _pageSize;
            var productsList = _context.Products
                .Skip((page - 1) * _pageSize)
                .Take(_pageSize)
                .Include(p => p.SubCategory);
            var model = new ProductsCollectionViewModel(productsList, null, null, page, pagesCount, 15);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}