using OnlineStore.Domain.Base;

namespace OnlineStore.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellation = default);

        Task<bool> ExistsAsync(int id, CancellationToken cancellation = default);

        Task<T?> GetAsync(int id, CancellationToken cancellation = default);

        Task<T?> CreateAsync(T? entity, CancellationToken cancellation = default);

        Task UpdateAsync(T? entity, CancellationToken cancellation = default);

        Task<bool> DeleteAsync(int id, CancellationToken cancellation = default);

    }
}
