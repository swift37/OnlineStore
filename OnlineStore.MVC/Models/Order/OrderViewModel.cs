using OnlineStore.MVC.Models.Enums;
using OnlineStore.MVC.Models.Product;

namespace OnlineStore.MVC.Models.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string? Number { get; set; }

        public ICollection<OrderItemViewModel> Items { get; set; } = new HashSet<OrderItemViewModel>();

        public OrderStatus Status { get; set; }

        public DateTimeOffset CreationDate { get; set; }

        public DateTimeOffset? PayDate { get; set; }

        public DateTimeOffset? ShippedDate { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public decimal Total { get; set; }

        public decimal ShippingCost { get; set; }

        public string? TrackingNumber { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Postcode { get; set; }

        public string? StreetAddress { get; set; }

        public string? Apartment { get; set; }

        public string? Notes { get; set; }
    }

    public class OrderItemViewModel
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public ProductViewModel? Product { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }
    }
}
