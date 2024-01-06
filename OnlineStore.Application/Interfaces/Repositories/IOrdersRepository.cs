using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces.Repositories
{
    public interface IOrdersRepository : IRepository<Order>
    {
        Task<int> CountAsync(CancellationToken cancellation = default);

        Task<int> CountForLastMonthAsync(CancellationToken cancellation = default);

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
