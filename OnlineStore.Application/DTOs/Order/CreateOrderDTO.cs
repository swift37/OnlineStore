using AutoMapper;
using OnlineStore.Application.Mapping;
using OnlineStore.Domain.Enums;

namespace OnlineStore.Application.DTOs.Order
{
    public class CreateOrderDTO : IMapWith<Domain.Entities.Order>
    {
        public ICollection<CreateOrderItemDTO> Items { get; set; } = new HashSet<CreateOrderItemDTO>();

        public OrderStatus Status { get; set; } = OrderStatus.NotPaid;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public decimal ShippingCost { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Postcode { get; set; }

        public string? StreetAddress { get; set; }

        public string? Apartment { get; set; }

        public string? Notes { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.Order, CreateOrderDTO>().ReverseMap();
    }

    public class CreateOrderItemDTO : IMapWith<Domain.Entities.OrderItem>
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.OrderItem, CreateOrderItemDTO>().ReverseMap();
    }
}
