using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.ShippingMethod
{
    public class UpdateShippingMethodDTO : BaseDTO, IMapWith<Domain.Entities.ShippingMethod>
    {
        public string? DisplayName { get; set; }

        public string? Name { get; set; }

        public decimal Price { get; set; }

        public string? Image { get; set; }

        public bool IsAvailable { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.ShippingMethod, UpdateShippingMethodDTO>().ReverseMap();
    }
}
