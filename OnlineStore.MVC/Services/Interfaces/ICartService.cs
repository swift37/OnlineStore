namespace OnlineStore.MVC.Services.Interfaces
{
    public interface ICartService
    {
        bool Add(int productId, int quantity = 1);

        bool Update(int productId, int quantity);

        bool Remove(int productId);

        void Clear();
    }
}
