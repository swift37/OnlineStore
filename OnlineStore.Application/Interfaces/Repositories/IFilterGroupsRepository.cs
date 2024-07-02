using OnlineStore.Domain;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces.Repositories
{
    public interface IFilterGroupsRepository : IRepository<FiltersGroup>
    {
        Task<FiltersGroup> GetCategoryFiltersGroupAsync(
            int categoryId, 
            CancellationToken cancellation = default);

        Task<FiltersGroup> GetCategoryFiltersGroupAsync(
            FiltersGroupOptions filtersGroupOptions,
            CancellationToken cancellation = default);
    }
}
