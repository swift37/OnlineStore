using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces.Repositories
{
    public interface ICategoriesRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetMainCategoriesAsync(CancellationToken cancellation = default);
    }
}
