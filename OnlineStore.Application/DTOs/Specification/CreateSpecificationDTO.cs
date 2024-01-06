using AutoMapper;
using OnlineStore.Application.DTOs.Product;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.Specification
{
    public class CreateSpecificationDTO : IMapWith<Domain.Entities.Specification>
    {
        public int SpecificationTypeId { get; set; }

        public string? Value { get; set; }

        public ICollection<ProductDTO> Products { get; set; } = new HashSet<ProductDTO>();

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.Specification, CreateSpecificationDTO>().ReverseMap();
    }
}
