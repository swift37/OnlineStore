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
            .Where(o => o.CreatedDate.Year == DateTime.Now.Year && 
                        o.CreatedDate.Month == DateTime.Now.Month)
            .CountAsync(cancellation)
            .ConfigureAwait(false);

        public async Task<Order> GetAsync(string? number, CancellationToken cancellation = default)
        {
            if (number is null) throw new ArgumentNullException(nameof(number));

            switch (Entities)
            {
                case DbSet<Order> dbSet:
                    return await dbSet.FindAsync(new object[] { number }, cancellation).ConfigureAwait(false)
                        ?? throw new NotFoundException(nameof(Order), number);
                case { } entities:
                    return await entities.FirstOrDefaultAsync(o => o.Number == number, cancellation).ConfigureAwait(false)
                        ?? throw new NotFoundException(nameof(Order), number);
                default:
                    throw new InvalidOperationException("Data source defenition failed.");
            }
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
    }
}
