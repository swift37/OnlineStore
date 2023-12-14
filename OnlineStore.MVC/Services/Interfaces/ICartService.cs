namespace OnlineStore.MVC.Services.Interfaces
{
    public interface ICartService
    {
        bool Add(int productId, int quantity = 1);

        bool Decrement(int productId);

        bool Remove(int productId);

        void Clear();
    }
}
