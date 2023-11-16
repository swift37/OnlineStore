using OnlineStore.Domain;

namespace OnlineStore.Application.Interfaces.Infrastructure
{
    public interface ICartStore
    {
        Cart? Cart { get; set; }
    }
}
