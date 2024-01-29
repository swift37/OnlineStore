using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Exeptions;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.DAL.Repositories;
using OnlineStore.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OnlineStore.Persistence.Repositories
{
    public class SubscribersRepository : Repository<Subscriber>, ISubscribersRepository
    {
        public SubscribersRepository(IApplicationDbContext context) : base(context) { }

        public async Task<Subscriber> GetAsync(string? email, CancellationToken cancellation = default)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email));

            return await Entities
                .SingleOrDefaultAsync(s => s.Email == email, cancellation)
            .ConfigureAwait(false)
                ?? throw new NotFoundException(nameof(Subscriber), email);
        }
    }
}
