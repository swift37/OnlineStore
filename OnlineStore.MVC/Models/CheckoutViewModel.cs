using OnlineStore.MVC.Models.Cart;
using OnlineStore.MVC.Models.Order;

namespace OnlineStore.MVC.Models
{
    public class CheckoutViewModel
    {
        public CartViewModel Cart { get; set; } = new();

        public CreateOrderViewModel Order { get; set; } = new();

        public bool UseUnauthCart { get; set; }
    }
}
