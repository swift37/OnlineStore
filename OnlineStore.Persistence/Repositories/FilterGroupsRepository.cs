using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using OnlineStore.Application.Exeptions;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.DAL.Repositories;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Persistence.Repositories
{
    public class FilterGroupsRepository : Repository<FiltersGroup>, IFilterGroupsRepository
    {
        protected override IQueryable<FiltersGroup> Entities => base.Entities
            .Include(filtersGroup => filtersGroup.SpecificationTypes)
                .ThenInclude(specificationType => specificationType.Values);

        public FilterGroupsRepository(IApplicationDbContext context) : base(context) { }

        public async Task<FiltersGroup> GetCategoryFiltersGroupAsync(int categoryId, CancellationToken cancellation = default) 
        {
            var filtersGroup = await Entities
                .FirstOrDefaultAsync(f => f.CategoryId == categoryId, cancellation)
                .ConfigureAwait(false) 
                ?? throw new NotFoundException(nameof(FiltersGroup), categoryId);

            foreach (var specificationType in filtersGroup.SpecificationTypes)
                foreach (var specification in specificationType.Values)
                    specification.ProductsCount = await _context.Products
                        .Where(p => p.CategoryId == categoryId && p.Specifications.Any(s => s.Id == specification.Id))
                        .CountAsync(cancellation);

            return filtersGroup;
        }

        public async Task<FiltersGroup> GetCategoryFiltersGroupAsync(int categoryId, IDictionary<int, ICollection<int>> filters, CancellationToken cancellation = default)
        {
            var filtersGroup = await Entities
                .FirstOrDefaultAsync(f => f.CategoryId == categoryId, cancellation)
                .ConfigureAwait(false)
                ?? throw new NotFoundException(nameof(FiltersGroup), categoryId);

            var productsQuery = _context.Products.Where(product =>
                product.CategoryId == categoryId &&
                product.Specifications.Select(spec => spec.SpecificationTypeId)
                .Intersect(filters.Keys)
                .Count() == filters.Keys.Count() &&
                product.Specifications.Select(spec => spec.Id)
                .Intersect(filters.Values.SelectMany(v => v))
                .Count() >=
                product.Specifications.Select(spec => spec.SpecificationTypeId)
                .Intersect(filters.Keys)
                .Count());

            foreach (var specificationType in filtersGroup.SpecificationTypes)
                foreach (var specification in specificationType.Values)
                    specification.ProductsCount = await productsQuery
                        .Where(p => p.Specifications.Any(s => s.Id == specification.Id))
                        .CountAsync(cancellation);

            return filtersGroup;
        }
    }
}
