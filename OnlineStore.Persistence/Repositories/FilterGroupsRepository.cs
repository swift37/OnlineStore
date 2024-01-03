using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineStore.Application.Exeptions;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.DAL.Repositories;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Persistence.Repositories
{
    public class FilterGroupsRepository : Repository<FiltersGroup>, IFilterGroupsRepository
    {
        public FilterGroupsRepository(IApplicationDbContext context) : base(context) { }

        public async Task<FiltersGroup> GetCategoryFiltersGroupAsync(int categoryId, CancellationToken cancellation = default) => 
            await Entities
            .FirstOrDefaultAsync(f => f.CategoryId == categoryId, cancellation)
            .ConfigureAwait(false)
            ?? throw new NotFoundException(nameof(FiltersGroup), categoryId);
    }
}
