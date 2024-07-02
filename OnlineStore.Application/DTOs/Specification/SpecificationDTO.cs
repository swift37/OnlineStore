using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Product;
using OnlineStore.Application.DTOs.SpecificationType;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.Specification
{
    public class SpecificationDTO : BaseDTO, IMapWith<Domain.Entities.Specification>
    {
        public int SpecificationTypeId { get; set; }

        public SpecificationTypeDTO? SpecificationType { get; set; }

        public string? Value { get; set; }

        public ICollection<ProductDTO> Products { get; set; } = new HashSet<ProductDTO>();

        public int ProductsCount { get; set; }

        public void Mapping(Profile profile) => 
            profile.CreateMap<Domain.Entities.Specification, SpecificationDTO>().ReverseMap();
    }
}
