using OnlineStore.Domain;

namespace OnlineStore.Interfaces.Infrastructure
{
    public interface ICartStore
    {
        Cart? Cart { get; set; }
    }
}
