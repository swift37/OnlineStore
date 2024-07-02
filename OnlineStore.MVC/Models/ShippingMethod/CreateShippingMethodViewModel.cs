namespace OnlineStore.MVC.Models.ShippingMethod
{
    public class CreateShippingMethodViewModel
    {
        public string? Name { get; set; }

        public string? DisplayName { get; set; }

        public decimal Price { get; set; }

        public string? Image { get; set; }

        public bool IsAvailable { get; set; }
    }
}
