using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces.Repositories
{
    public interface IFilterGroupsRepository : IRepository<FiltersGroup>
    {
        Task<FiltersGroup> GetCategoryFiltersGroupAsync(
            int categoryId, 
            CancellationToken cancellation = default);

        Task<FiltersGroup> GetCategoryFiltersGroupAsync(
            int categoryId,
            IDictionary<int, ICollection<int>> filters,
            CancellationToken cancellation = default);
    }
}
