using OnlineStore.MVC.Models.Order;

namespace OnlineStore.MVC.Models.PaymentMethod
{
    public class CreatePaymentMethodViewModel
    {
        public string? Name { get; set; }

        public string? DisplayName { get; set; }

        public string? Image { get; set; }

        public bool IsAvailable { get; set; }
    }
}
