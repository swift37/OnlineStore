using OnlineStore.Application.DTOs.Base;
using OnlineStore.Domain;
using OnlineStore.Domain.Enums;

namespace OnlineStore.Application.DTOs.Order
{
    public class UpdateOrderDTO : BaseDTO
    {
        public ICollection<UpdateOrderItemDTO> Items { get; set; } = new HashSet<UpdateOrderItemDTO>();

        public OrderStatus Status { get; set; }

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

    public class UpdateOrderItemDTO : BaseDTO
    {
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }
    }
}
