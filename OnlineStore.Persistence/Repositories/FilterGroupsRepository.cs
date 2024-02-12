using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using OnlineStore.Application.Exeptions;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.DAL.Repositories;
using OnlineStore.Domain;
using OnlineStore.Domain.Entities;
using System.Linq;

namespace OnlineStore.Persistence.Repositories
{
    public class FilterGroupsRepository : Repository<FiltersGroup>, IFilterGroupsRepository
    {
        protected override IQueryable<FiltersGroup> Entities => base.Entities
            .Include(filtersGroup => filtersGroup.SpecificationTypes)
                .ThenInclude(specificationType => specificationType.Values);

        public FilterGroupsRepository(IApplicationDbContext context) : base(context) { }

        public async Task<FiltersGroup> GetCategoryFiltersGroupAsync(
            int categoryId, 
            CancellationToken cancellation = default) 
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

        public async Task<FiltersGroup> GetCategoryFiltersGroupAsync(
            FiltersGroupOptions options, 
            CancellationToken cancellation = default)
        {
            var filtersGroup = await Entities
                .FirstOrDefaultAsync(f => f.CategoryId == options.CategoryId, cancellation)
                .ConfigureAwait(false)
                ?? throw new NotFoundException(nameof(FiltersGroup), options.CategoryId);

            var productsQuery = _context.Products
                .Where(product => product.CategoryId == options.CategoryId);

            foreach (var specificationType in filtersGroup.SpecificationTypes)
            {
                var specificationTypeIds = options.AppliedFilters
                        .Where(af => af.Key != specificationType.Id)
                        .Select(af => af.Key);

                var specificationIds = options.AppliedFilters
                        .Where(af => af.Key != specificationType.Id)
                        .SelectMany(af => af.Value);

                foreach (var specification in specificationType.Values)
                    specification.ProductsCount = await productsQuery
                        .Where(product => product.Specifications.Select(spec => spec.SpecificationTypeId)
                        .Intersect(options.AppliedFilters.Keys)
                        .Count().Equals(options.AppliedFilters.Keys.Count) &&
                        product.Specifications.Select(spec => spec.Id)
                        .Intersect(specificationIds)
                        .Count() >=
                        product.Specifications.Select(spec => spec.SpecificationTypeId)
                        .Intersect(specificationTypeIds)
                        .Count())
                        .Where(p => p.Specifications.Any(s => s.Id == specification.Id))
                        .CountAsync(cancellation);
            }

            return filtersGroup;
        }
    }
}
