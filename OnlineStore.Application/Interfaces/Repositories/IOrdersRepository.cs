using OnlineStore.Domain;

namespace OnlineStore.Application.Interfaces.Repositories
{
    public interface IOrdersRepository : IRepository<Order>
    {
        Task<Order> GetAsync(
            string number,
            CancellationToken cancellation = default);

        Task<IEnumerable<Order>> GetUserOrdersAsync(
            Guid userId, 
            CancellationToken cancellation = default);

        Task<Order> GetUserOrderAsync(
            int id,
            Guid userId,
            CancellationToken cancellation = default);
    }
}
