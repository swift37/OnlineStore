using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Interfaces;
using OnlineStore.DAL.Repositories;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Persistence.Repositories
{
    public class SpecificationsRepository : Repository<Specification>
    {
        protected override IQueryable<Specification> Entities => base.Entities
            .Include(specification => specification.SpecificationType);

        public SpecificationsRepository(IApplicationDbContext context) : base(context) { }
    }
}
