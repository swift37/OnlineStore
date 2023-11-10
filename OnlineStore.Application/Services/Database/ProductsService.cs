using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Mapping;
using OnlineStore.Application.Models;
using OnlineStore.DAL.Context;
using OnlineStore.Domain;

namespace OnlineStore.Application.Services.Database
{
    public class ProductsService
    {
        private readonly ApplicationDbContext _context;

        public ProductsService(ApplicationDbContext context) => _context = context;

        public async Task<ProductDTO?> GetProduct(int id) =>
            (await _context.Products.Include(p => p.Specifications).SingleOrDefaultAsync(p => p.Id == id))?.ToDTO();

        public async Task<ProductsCollectionDTO?> GetProductByCategory(
            int catId,
            int page = 1,
            int itemsPerPage = 15,
            SortParameters sortBy = SortParameters.Default, 
            CancellationToken cancellation = default)
        {
            if (itemsPerPage > 30 || itemsPerPage < 15) itemsPerPage = 15;

            var category = await _context.Categories
                .Include(c => c.Parent)
                .SingleOrDefaultAsync(c => c.Id == catId, cancellation).ConfigureAwait(false);
            if (category is null) return null;

            var query = _context.Products.Where(p => p.Category == null ? false : p.Category.Id == category.Id);
            var pagesCount = (await query.CountAsync(cancellation) + itemsPerPage - 1) / itemsPerPage;
            var productsList = query
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Include(p => p.Category).AsEnumerable();

            var productsSortedList = SortProducts(productsList, sortBy);

            var products = new ProductsCollectionDTO 
            {
                Products = productsSortedList.ToDTO(),
                Category = category.ToDTO(),
                CurrentPage = page,
                TotalPages = pagesCount,
                ItemsPerPage = itemsPerPage 
            };

            return products;
        }

        private IEnumerable<Product> SortProducts(IEnumerable<Product> products, SortParameters sortBy)
        {
            switch (sortBy)
            {
                default:
                    return products;
                case SortParameters.RatingDescending:
                    return products.OrderByDescending(p => p.Rating);
                case SortParameters.PriceAscending:
                    return products.OrderBy(p => p.UnitPrice);
                case SortParameters.PriceDescending:
                    return products.OrderByDescending(p => p.UnitPrice);
            }
        }

        public async Task<int> CreateProduct(ProductDTO? product, CancellationToken cancellation = default)
        {
            var newProduct = product?.FromDTO();
            if (newProduct is null) throw new ArgumentNullException(nameof(newProduct));

            _context.Products.Entry(newProduct).State = EntityState.Added;
            await _context.SaveChangesAsync(cancellation).ConfigureAwait(false);
            return newProduct.Id;
        }

        public async Task UpdateProduct(ProductDTO? product, CancellationToken cancellation = default)
        {
            var updatedProduct = product?.FromDTO();
            if (updatedProduct is null) throw new ArgumentNullException(nameof(updatedProduct));

            _context.Products.Entry(updatedProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellation).ConfigureAwait(false);
        }

        public async Task<bool> DeleteProduct(int id, CancellationToken cancellation = default)
        {
            var deletedProduct = await _context.Products
                .FindAsync(new object[] { id }, cancellation)
                .ConfigureAwait(false);
            if (deletedProduct is not { }) return false;

            _context.Products.Entry(deletedProduct).State = EntityState.Deleted;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        public async Task<IEnumerable<CategoryDTO?>> GetCategoties(CancellationToken cancellation = default) =>
            (await _context.Categories.ToListAsync()).ToDTO();

        public async Task<CategoryDTO?> GetCategoty(int id, CancellationToken cancellation = default) =>
            (await _context.Categories
            .Include(c => c.Parent)
            .Include(c => c.Subcategories)
            .Include(c => c.Products)
            .SingleOrDefaultAsync(p => p.Id == id, cancellation))?
            .ToDTO();

        public async Task<int> CreateCategory(CategoryDTO? category, CancellationToken cancellation = default)
        {
            var newCategory = category?.FromDTO();
            if (newCategory is null) throw new ArgumentNullException(nameof(newCategory));

            _context.Categories.Entry(newCategory).State = EntityState.Added;
            await _context.SaveChangesAsync(cancellation).ConfigureAwait(false);
            return newCategory.Id;
        }

        public async Task UpdateCategory(CategoryDTO? category, CancellationToken cancellation = default)
        {
            var updatedCategory = category?.FromDTO();
            if (updatedCategory is null) throw new ArgumentNullException(nameof(updatedCategory));

            _context.Categories.Entry(updatedCategory).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellation).ConfigureAwait(false);
        }

        public async Task<bool> DeleteCategory(int id, CancellationToken cancellation = default)
        {
            var deletedCategory = await _context.Categories
                .FindAsync(new object[] { id }, cancellation)
                .ConfigureAwait(false);
            if (deletedCategory is not { }) return false;

            _context.Categories.Entry(deletedCategory).State = EntityState.Deleted;
            await _context.SaveChangesAsync(cancellation).ConfigureAwait(false);
            return true;
        }
    }

    public enum SortParameters
    {
        Default,
        RatingDescending,
        PriceAscending,
        PriceDescending
    }
}
