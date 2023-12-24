using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.DAL.Repositories;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Persistence.Repositories
{
    public class CategoriesRepository : Repository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(IApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Category>> GetMainCategoriesAsync(CancellationToken cancellation = default) =>
            await Entities
                .Where(category => category.IsRootCategory)
                .Include(category => category.ChildCategories)
                .ToArrayAsync(cancellation)
                .ConfigureAwait(false);
    }
}
