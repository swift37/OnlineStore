using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Exeptions;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.DAL.Repositories;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Persistence.Repositories
{
    public class SubscribersRepository : Repository<Subscriber>, ISubscribersRepository
    {
        public SubscribersRepository(IApplicationDbContext context) : base(context) { }

        public async Task<Subscriber> GetAsync(string? email, CancellationToken cancellation = default)
        {
            if (email is null) throw new ArgumentNullException(nameof(email));

            switch (Entities)
            {
                case DbSet<Subscriber> dbSet:
                    return await dbSet.FindAsync(new object[] { email }, cancellation).ConfigureAwait(false)
                        ?? throw new NotFoundException(nameof(Subscriber), email);
                case { } entities:
                    return await entities.FirstOrDefaultAsync(s => s.Email == email, cancellation).ConfigureAwait(false)
                        ?? throw new NotFoundException(nameof(Subscriber), email);
                default:
                    throw new InvalidOperationException("Data source defenition failed.");
            }
        }
    }
}
