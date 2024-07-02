using OnlineStore.MVC.Models.Cart;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface ICartStorage
    {
        CartViewModel? Cart { get; set; }

        CartViewModel? GetUnauthCart();
    }
}
