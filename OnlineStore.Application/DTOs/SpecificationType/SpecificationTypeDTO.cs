using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Specification;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.SpecificationType
{
    public class SpecificationTypeDTO : BaseDTO, IMapWith<Domain.Entities.SpecificationType>
    {
        public string? Name { get; set; }

        public string? DisplayName { get; set; }

        public bool IsMain { get; set; }

        public ICollection<SpecificationDTO> Values { get; set; } = new HashSet<SpecificationDTO>();

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.SpecificationType, SpecificationTypeDTO>().ReverseMap();
    }
}
