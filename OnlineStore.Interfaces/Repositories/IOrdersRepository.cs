using OnlineStore.Domain;

namespace OnlineStore.Interfaces.Repositories
{
    public interface IOrdersRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetUserOrders(Guid userId);
    }
}
