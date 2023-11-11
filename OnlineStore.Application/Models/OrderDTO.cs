using OnlineStore.Domain;

namespace OnlineStore.Application.Models
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public ICollection<OrderItemDTO?> Items { get; set; } = new HashSet<OrderItemDTO?>();

        public OrderStatus Status { get; set; } = OrderStatus.NotPaid;

        public DateTime CreatedDate { get; set; }

        public DateTime? PayDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

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

    public class OrderItemDTO
    {
        public int Id { get; set; }

        public OrderDTO? Order { get; set; }

        public ProductDTO? Product { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }
    }
}
