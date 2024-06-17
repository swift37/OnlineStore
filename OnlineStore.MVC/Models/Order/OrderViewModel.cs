using OnlineStore.MVC.Models.Enums;
using OnlineStore.MVC.Models.PaymentMethod;
using OnlineStore.MVC.Models.Product;
using OnlineStore.MVC.Models.ShippingMethod;

namespace OnlineStore.MVC.Models.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string? Number { get; set; }

        public ICollection<OrderItemViewModel> Items { get; set; } = new HashSet<OrderItemViewModel>();

        public OrderStatus Status { get; set; }

        public DateTimeOffset CreatingDate { get; set; }

        public DateTimeOffset? PaymentDate { get; set; }

        public DateTimeOffset? ShippingDate { get; set; }

        public DateTimeOffset? DeliveryDate { get; set; }

        public PaymentMethodViewModel? PaymentMethod { get; set; }

        public string? PaymentSession { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public decimal Total { get; set; }

        public ShippingMethodViewModel? ShippingMethod { get; set; }

        public string? TrackingNumber { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Postcode { get; set; }

        public string? StreetAddress { get; set; }

        public string? Apartment { get; set; }

        public string? Notes { get; set; }

        public int ItemsQuantity => Items.Sum(i => i.Quantity);

        public decimal Discount => Items.Sum(i => i.Discount);

        public decimal Subtotal => Items.Sum(i => i.UnitPrice * i.Quantity);

        public decimal CalculatedTotal => Total == default 
            ? Subtotal - Discount + (ShippingMethod?.Price ?? default) 
            : Total;
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

        public decimal Subtotal => UnitPrice * Quantity;

        public decimal Total => (UnitPrice - Discount) * Quantity;
    }
}
