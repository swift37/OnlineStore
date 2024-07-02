using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces.Infrastructure
{
    public interface IOrderNumbersProvider
    {
        Task<string> GenerateNumberAsync(Order order, CancellationToken cancellation = default);
    }
}
