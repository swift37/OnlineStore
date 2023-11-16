namespace OnlineStore.Application.Interfaces.Infrastructure
{
    public interface ICartService
    {
        void Add(int id);

        void Decrement(int id);

        void Remove(int id);

        void Clear();
    }
}
