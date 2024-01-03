using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Exeptions;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.Repositories
{
    public class ProductsRepository : Repository<Product>, IProductsRepository
    {
        protected override IQueryable<Product> Entities => base.Entities
            .Include(product => product.Category);

        public ProductsRepository(IApplicationDbContext context) : base(context) { }

        public async Task<ProductsPage> GetFilteredProductsAsync(
            ProductsFilteringOptions options,
            CancellationToken cancellation = default)
        {
            if (options.ItemsPerPage > 30 || options.ItemsPerPage < 15) options.ItemsPerPage = 15;

            var category = await _context.Categories
                .SingleOrDefaultAsync(c => c.Id == options.CategoryId, cancellation)
                .ConfigureAwait(false);

            if (category is null) throw new NotFoundException(nameof(Category), options.CategoryId);

            var query = Entities.Where(p =>
                p.CategoryId == options.CategoryId &&
                p.UnitPrice - p.Discount >= options.MinPrice &&
                p.UnitPrice - p.Discount <= options.MaxPrice &&
                p.Specifications
                    .DefaultIfEmpty()
                    .IntersectBy(options.Specifications.Select(s => s.Id), s => s.Id)
                    .Count() == options.Specifications.Count);

            var pagesCount = 
                (await query.CountAsync(cancellation) + options.ItemsPerPage - 1) 
                / options.ItemsPerPage;

            var productsCollection = await query
                .Skip((options.PageNumber - 1) * options.ItemsPerPage)
                .Take(options.ItemsPerPage)
                .Include(p => p.Category)
                .ToArrayAsync();

            var products = new ProductsPage
            {
                Products = productsCollection,
                Category = category,
                CurrentPage = options.PageNumber,
                TotalPages = pagesCount,
                ItemsPerPage = options.ItemsPerPage
            };

            return products;
        }

        public async Task<int> GetCountByFilterAsync(int categoryId, int specificationId, CancellationToken cancellation = default) => 
            await Entities
            .Where(p => p.CategoryId == categoryId && p.Specifications
                .Any(s => s.Id == specificationId))
            .CountAsync(cancellation)
            .ConfigureAwait(false);
    }
}
