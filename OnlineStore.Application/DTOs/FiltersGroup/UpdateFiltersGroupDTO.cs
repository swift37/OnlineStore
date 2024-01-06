using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Category;
using OnlineStore.Application.DTOs.SpecificationType;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.FiltersGroup
{
    public class UpdateFiltersGroupDTO : BaseDTO, IMapWith<Domain.Entities.FiltersGroup>
    {
        public int CategoryId { get; set; }

        public CategoryDTO? Category { get; set; }

        public ICollection<SpecificationTypeDTO> SpecificationTypes { get; set; } = new HashSet<SpecificationTypeDTO>();

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.FiltersGroup, UpdateFiltersGroupDTO>().ReverseMap();
    }
}
