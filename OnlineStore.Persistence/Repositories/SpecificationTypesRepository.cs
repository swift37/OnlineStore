using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Interfaces;
using OnlineStore.DAL.Repositories;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Persistence.Repositories
{
    public class SpecificationTypesRepository : Repository<SpecificationType>
    {
        protected override IQueryable<SpecificationType> Entities => base.Entities
            .Include(specificationType => specificationType.Values);

        public SpecificationTypesRepository(IApplicationDbContext context) : base(context) { }
    }
}
