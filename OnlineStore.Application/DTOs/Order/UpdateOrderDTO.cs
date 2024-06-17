using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.Mapping;
using OnlineStore.Domain.Enums;

namespace OnlineStore.Application.DTOs.Order
{
    public class UpdateOrderDTO : BaseDTO, IMapWith<Domain.Entities.Order>
    {
        public OrderStatus Status { get; set; }

        public DateTimeOffset? ShippingDate { get; set; }

        public DateTimeOffset? DeliveryDate { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public decimal Total { get; set; }

        public int? PaymentMethodId { get; set; }

        public int? ShippingMethodId { get; set; }

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
}
