using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.ProductTag
{
    public class UpdateProductTagDTO : BaseDTO, IMapWith<Domain.Entities.ProductTag>
    {
        public string? Name { get; set; }

        public string? DisplayName { get; set; }

        public string? ColorHex { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.ProductTag, UpdateProductTagDTO>().ReverseMap();
    }
}
