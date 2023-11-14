using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.Context;
using OnlineStore.Domain;
using OnlineStore.Interfaces.Repositories;

namespace OnlineStore.DAL.Repositories
{
    public class OrdersRepository : Repository<Order>, IOrdersRepository
    {
        public OrdersRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Order>> GetUserOrders(Guid userId) => await DbSet
            .Where(o => o.UserId == userId)
            .ToArrayAsync();
    }
}
