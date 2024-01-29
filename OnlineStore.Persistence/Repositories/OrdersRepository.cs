using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Exeptions;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.Repositories
{
    public class OrdersRepository : Repository<Order>, IOrdersRepository
    {
        public OrdersRepository(IApplicationDbContext context) : base(context) { }

        public async Task<int> CountAsync(CancellationToken cancellation = default) => await Entities
            .CountAsync(cancellation)
            .ConfigureAwait(false);

        public async Task<int> CountForLastMonthAsync(CancellationToken cancellation = default) => await Entities
            .Where(o => o.CreationDate.Year == DateTime.Now.Year && 
                        o.CreationDate.Month == DateTime.Now.Month)
            .CountAsync(cancellation)
            .ConfigureAwait(false);

        public async Task<Order> GetAsync(string? number, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(number)) throw new ArgumentNullException(nameof(number));

            return await Entities
                .SingleOrDefaultAsync(o => o.Number == number, cancellation)
                .ConfigureAwait(false)
                ?? throw new NotFoundException(nameof(Order), number);
        }

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

        public async Task<IEnumerable<Order>> GetUserOrdersAwaitingReviewAsync(
            Guid userId,
            CancellationToken cancellation = default) => await Entities
            .Where(o => o.UserId == userId)
            .Include(o => o.Items
                .Where(i => !i.Product!.Reviews.Any(r => r.UserId == userId)))
            .ToArrayAsync(cancellation)
            .ConfigureAwait(false);
    }
}
