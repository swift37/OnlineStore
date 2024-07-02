using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Product;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.ProductTag
{
    public class ProductTagDTO : BaseDTO, IMapWith<Domain.Entities.ProductTag>
    {
        public string? Name { get; set; }

        public string? DisplayName { get; set; }

        public string? ColorHex { get; set; }

        public ICollection<ProductDTO> Products { get; set; } = new HashSet<ProductDTO>();

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.ProductTag, ProductTagDTO>().ReverseMap();
    }
}
