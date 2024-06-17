using OnlineStore.MVC.Models.Order;

namespace OnlineStore.MVC.Models.ShippingMethod
{
    public class ShippingMethodViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? DisplayName { get; set; }

        public decimal Price { get; set; }

        public string? Image { get; set; }

        public ICollection<OrderViewModel> Orders { get; set; } = new HashSet<OrderViewModel>();

        public bool IsAvailable { get; set; }
    }
}
