using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces.Repositories
{
    public interface ISubscribersRepository : IRepository<Subscriber>
    {
        Task<Subscriber> GetAsync(
            string? email,
            CancellationToken cancellation = default);
    }
}
