using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Order;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.ShippingMethod
{
    public class ShippingMethodDTO: BaseDTO, IMapWith<Domain.Entities.ShippingMethod>
    {
        public string? DisplayName { get; set; }

        public string? Name { get; set; }

        public decimal Price { get; set; }

        public string? Image { get; set; }

        public ICollection<OrderDTO> Orders { get; set; } = new HashSet<OrderDTO>();

        public bool IsAvailable { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.ShippingMethod, ShippingMethodDTO>().ReverseMap();
    }
}
