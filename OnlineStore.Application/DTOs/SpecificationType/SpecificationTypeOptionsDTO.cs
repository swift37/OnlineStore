using AutoMapper;
using OnlineStore.Application.Mapping;
using OnlineStore.Domain;

namespace OnlineStore.Application.DTOs.SpecificationType
{
    public class SpecificationTypeOptionsDTO : IMapWith<SpecificationTypeOptions>
    {
        public int Id { get; set; }

        public IDictionary<int, ICollection<int>> AppliedFilters { get; set; } = new Dictionary<int, ICollection<int>>();

        public void Mapping(Profile profile) =>
            profile.CreateMap<SpecificationTypeOptions, SpecificationTypeOptionsDTO>().ReverseMap();
    }
}
