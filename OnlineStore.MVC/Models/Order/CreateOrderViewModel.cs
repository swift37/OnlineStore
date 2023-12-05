using OnlineStore.MVC.Models.Enums;

namespace OnlineStore.MVC.Models.Order
{
    public class CreateOrderViewModel
    {
        public ICollection<CreateOrderItemViewModel> Items { get; set; } = new HashSet<CreateOrderItemViewModel>();

        public OrderStatus Status { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public double ShippingCost { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Postcode { get; set; }

        public string? StreetAddress { get; set; }

        public string? Apartment { get; set; }

        public string? Notes { get; set; }
    }

    public class CreateOrderItemViewModel
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public double UnitPrice { get; set; }

        public double Discount { get; set; }
    }
}
