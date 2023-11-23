using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Exeptions;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.DAL.Context;
using OnlineStore.Domain;
using static OnlineStore.Application.Interfaces.Repositories.IProductsRepository;

namespace OnlineStore.DAL.Repositories
{
    public class ProductsRepository : Repository<Product>, IProductsRepository
    {
        public ProductsRepository(ApplicationDbContext context) : base(context) { }

        public async Task<ProductsPage> GetProductsByCategoryAsync(
            int categoryId, 
            int page = 1, 
            int itemsPerPage = 15, 
            SortParameters sortBy = SortParameters.Default,
            CancellationToken cancellation = default)
        {
            if (itemsPerPage > 30 || itemsPerPage < 15) itemsPerPage = 15;

            var category = await _context.Categories
                .SingleOrDefaultAsync(c => c.Id == categoryId, cancellation)
                .ConfigureAwait(false);

            if (category is null) throw new NotFoundException(nameof(Category), categoryId);

            var query = Entities.Where(p => p.Category == null ? false : p.Category.Id == category.Id);
            var pagesCount = (await query.CountAsync(cancellation) + itemsPerPage - 1) / itemsPerPage;
            var productsList = query
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Include(p => p.Category).AsEnumerable();

            var productsSortedList = SortProducts(productsList, sortBy);

            var products = new ProductsPage
            {
                Products = productsSortedList,
                Category = category,
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
    }
}
