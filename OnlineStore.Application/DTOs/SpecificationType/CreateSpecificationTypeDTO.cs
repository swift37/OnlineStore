using AutoMapper;
using OnlineStore.Application.DTOs.Specification;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.SpecificationType
{
    public class CreateSpecificationTypeDTO : IMapWith<Domain.Entities.SpecificationType>
    {
        public string? Name { get; set; }

        public string? DisplayName { get; set; }

        public bool IsMain { get; set; }

        public ICollection<CreateSpecificationDTO> Values { get; set; } = new HashSet<CreateSpecificationDTO>();

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.SpecificationType, CreateSpecificationTypeDTO>().ReverseMap();
    }
}
