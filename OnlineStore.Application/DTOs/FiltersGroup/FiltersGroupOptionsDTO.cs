using AutoMapper;
using OnlineStore.Application.Mapping;
using OnlineStore.Domain;

namespace OnlineStore.Application.DTOs.FiltersGroup
{
    public class FiltersGroupOptionsDTO : IMapWith<FiltersGroupOptions>
    {
        public int CategoryId { get; set; }

        public int AppliedMinPrice { get; set; }

        public int AppliedMaxPrice { get; set; }

        public IDictionary<int, ICollection<int>> AppliedFilters { get; set; } = new Dictionary<int, ICollection<int>>();

        public void Mapping(Profile profile) =>
            profile.CreateMap<FiltersGroupOptions, FiltersGroupOptionsDTO>().ReverseMap();
    }
}
