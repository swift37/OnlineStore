using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Exeptions;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.DAL.Repositories;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Persistence.Repositories
{
    public class SpecificationsRepository : Repository<Specification>, ISpecificationsRepository
    {
        protected override IQueryable<Specification> Entities => base.Entities
            .Include(specification => specification.SpecificationType);

        public SpecificationsRepository(IApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Specification>> GetManyAsync(
            IEnumerable<int> ids,
            CancellationToken cancellation = default) => await Entities
            .Where(s => ids.Contains(s.Id))
            .ToArrayAsync()
            .ConfigureAwait(false)
            ?? throw new NotFoundException(nameof(Specification), ids);
    }
}
