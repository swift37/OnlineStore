using OnlineStore.Domain;

namespace OnlineStore.Application.Interfaces.Repositories
{
    public interface IOrdersRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetUserOrdersAsync(Guid userId);

        Task<Order?> GetUserOrderAsync(
            int id,
            Guid userId,
            CancellationToken cancellation = default);
    }
}
