using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces.Infrastructure
{
    public interface ICartStore
    {
        Cart? Cart { get; set; }
    }
}
