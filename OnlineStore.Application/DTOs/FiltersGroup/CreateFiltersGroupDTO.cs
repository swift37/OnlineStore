using AutoMapper;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.FiltersGroup
{
    public class CreateFiltersGroupDTO : IMapWith<Domain.Entities.FiltersGroup>
    {
        public int CategoryId { get; set; }

        public ICollection<int> SpecificationTypeIds { get; set; } = new HashSet<int>();

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.FiltersGroup, CreateFiltersGroupDTO>().ReverseMap();
    }
}
