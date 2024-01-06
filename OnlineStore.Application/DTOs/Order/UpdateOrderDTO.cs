using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.Mapping;
using OnlineStore.Domain.Enums;

namespace OnlineStore.Application.DTOs.Order
{
    public class UpdateOrderDTO : BaseDTO, IMapWith<Domain.Entities.Order>
    {
        public string? Number { get; set; }

        public ICollection<UpdateOrderItemDTO> Items { get; set; } = new HashSet<UpdateOrderItemDTO>();

        public OrderStatus Status { get; set; }

        public DateTime? PayDate { get; set; }

        public DateTime? ShippedDate { get; set; }

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

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.Order, UpdateOrderDTO>().ReverseMap();
    }

    public class UpdateOrderItemDTO : BaseDTO, IMapWith<Domain.Entities.OrderItem>
    {
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.OrderItem, UpdateOrderItemDTO>().ReverseMap();
    }
}
