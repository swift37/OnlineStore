using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Exeptions;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain;

namespace OnlineStore.DAL.Repositories
{
    public class OrdersRepository : Repository<Order>, IOrdersRepository
    {
        public OrdersRepository(IApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(
            Guid userId, 
            CancellationToken cancellation = default) => await Entities
            .Where(o => o.UserId == userId)
            .ToArrayAsync(cancellation);

        public async Task<Order> GetUserOrderAsync(
            int id, 
            Guid userId, 
            CancellationToken cancellation = default) => 
            await Entities.FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId)
            .ConfigureAwait(false)
            ?? throw new NotFoundException(nameof(Order), id);
    }
}
