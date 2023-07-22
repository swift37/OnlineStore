using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Domain;
using OnlineStore.Models;

namespace OnlineStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Categories() 
        {
            return View(_context.Categories.Include(c => c.SubCategories).ToArray()); 
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
            return RedirectToAction("Categories");
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Id == id);
            if (category is null) return RedirectToAction("NotFound", "Error");

            return View(category);
        }

        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Categories");
        }

        [HttpGet]
        public IActionResult CreateSubCategory()
        {
            var model = new SubCategoryViewModel();
            model.AvailableCategories = new SelectList(
                _context.Categories,
                nameof(Category.Id),
                nameof(Category.Name));
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateSubCategory(SubCategoryViewModel model)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Id == model.CategoryId);

            if (category is null) return View();

            var subcategory = new SubCategory();
            subcategory.Name = model.Name;
            subcategory.Category = category;

            _context.Entry(subcategory).State = EntityState.Added;
            _context.SaveChanges();
            return RedirectToAction("Categories");
        }

        [HttpGet]
        public IActionResult EditSubCategory(int id)
        {
            var subcategory = _context.SubCategories.SingleOrDefault(c => c.Id == id);
            if (subcategory is null) return RedirectToAction("NotFound", "Error");

            var model = new SubCategoryViewModel
            {
                Id = subcategory.Id,
                Name = subcategory.Name,
                CategoryId = subcategory.CategoryId,
                AvailableCategories = new SelectList(
                    _context.Categories, 
                    nameof(Category.Id), 
                    nameof(Category.Name))
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditSubCategory(SubCategoryViewModel model)
        {
            var subcategory = _context.SubCategories.SingleOrDefault(c => c.Id == model.Id);
            var category = _context.Categories.SingleOrDefault(c => c.Id == model.CategoryId);

            if (subcategory is null || category is null) return RedirectToAction("NotFound", "Error");

            subcategory.Name = model.Name;
            subcategory.Category = category;

            _context.Entry(subcategory).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Categories");
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            var model = new ProductViewModel();
            model.AvailableCategories = new SelectList(
                _context.Categories,
                nameof(Category.Id),
                nameof(Category.Name));

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductViewModel model)
        {
            var product = new Product 
            { 
                Name = model.Name,
                Description = model.Description,
                UnitPrice = model.UnitPrice,
                UnitsInStock = model.UnitsInStock,
                CategoryId = model.CategoryId,
                SubCategoryId = model.SubCategoryId
            };

            if (model.ImageFile != null && 
                model.ImageFile.Length > 0 && 
                model.ImageFile.ContentType.Contains("image"))
            {
                
                var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Resources", "ProductImages", model.ImageFile.FileName);
                using (Stream stream = new FileStream(path, FileMode.Create))
                    model.ImageFile.CopyTo(stream);
                product.Image = path;
            }

            _context.Entry(product).State = EntityState.Added;
            _context.SaveChanges();
            return RedirectToAction("Categories");
        }

        public IActionResult EditProduct(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product is null) return RedirectToAction("NotFound", "Error");

            var model = new ProductViewModel
            {
                Id = subcategory.Id,
                Name = subcategory.Name,
                CategoryId = subcategory.CategoryId,
                AvailableCategories = new SelectList(
        _context.Categories,
        nameof(Category.Id),
        nameof(Category.Name))
            };

            return RedirectToAction("Categories");
        }

        public IActionResult GetSubCategories(int id)
        {
            var category = _context.Categories.Include(c => c.SubCategories).SingleOrDefault(c => c.Id == id);
            if (category is null) return Json(false);

            var subcategories = new SelectList(category.SubCategories, nameof(SubCategory.Id), nameof(SubCategory.Name));
            return Json(subcategories);
        }
    }
}
