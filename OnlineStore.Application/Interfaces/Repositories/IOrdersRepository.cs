using OnlineStore.Domain;

namespace OnlineStore.Application.Interfaces.Repositories
{
    public interface IOrdersRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetUserOrders(Guid userId);
    }
}
