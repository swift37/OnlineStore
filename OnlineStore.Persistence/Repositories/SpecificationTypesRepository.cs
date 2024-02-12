using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Exeptions;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.DAL.Repositories;
using OnlineStore.Domain;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Persistence.Repositories
{
    public class SpecificationTypesRepository : Repository<SpecificationType>, ISpecificationTypesRepository
    {
        protected override IQueryable<SpecificationType> Entities => base.Entities
            .Include(specificationType => specificationType.Values);

        public SpecificationTypesRepository(IApplicationDbContext context) : base(context) { }

        public async Task<SpecificationType> GetAsync(
            SpecificationTypeOptions options,
            CancellationToken cancellation = default)
        {
            var specificationType = await Entities
                .FirstOrDefaultAsync(s => s.Id == options.Id, cancellation)
                .ConfigureAwait(false)
                ?? throw new NotFoundException(nameof(SpecificationType), options.Id);

            var productsQuery = Entities
                .Where(s => s.Id == options.Id)
                .SelectMany(s => s.Values.SelectMany(v => v.Products));

            var specificationTypeIds = options.AppliedFilters
                        .Where(af => af.Key != options.Id)
                        .Select(af => af.Key);

            var specificationIds = options.AppliedFilters
                    .Where(af => af.Key != options.Id)
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

            return specificationType;
        }
    }
}
