using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces.Repositories
{
    public interface ISpecificationsRepository : IRepository<Specification>
    {
        Task<IEnumerable<Specification>> GetManyAsync(
            IEnumerable<int> ids,
            CancellationToken cancellation = default);
    }
}
