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

        public async Task<ProductsPage> GetProductsByCategoryAsync(
            int categoryId, 
            int page = 1, 
            int itemsPerPage = 15,
            CancellationToken cancellation = default)
        {
            if (itemsPerPage > 30 || itemsPerPage < 15) itemsPerPage = 15;

            var category = await _context.Categories
                .SingleOrDefaultAsync(c => c.Id == categoryId, cancellation)
                .ConfigureAwait(false);

            if (category is null) throw new NotFoundException(nameof(Category), categoryId);

            var query = Entities.Where(p => p.Category == null ? false : p.Category.Id == category.Id);
            var pagesCount = (await query.CountAsync(cancellation) + itemsPerPage - 1) / itemsPerPage;
            var productsList = await query
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Include(p => p.Category).ToArrayAsync();

            var products = new ProductsPage
            {
                Products = productsList,
                Category = category,
                CurrentPage = page,
                TotalPages = pagesCount,
                ItemsPerPage = itemsPerPage
            };

            return products;
        }
    }
}
