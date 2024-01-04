using OnlineStore.Application.DTOs.FiltersGroup;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Mapping
{
    public static class FiltersGroupMapper
    {
        public static FiltersGroupDTO ToDTO(this FiltersGroup filtersGroup) => new FiltersGroupDTO
        {
            Id = filtersGroup.Id,
            CategoryId = filtersGroup.CategoryId,
            Category = filtersGroup.Category?.ToDTO(),
            SpecificationTypes = filtersGroup.SpecificationTypes.ToDTO().ToArray()
        };

        public static FiltersGroup FromDTO(this CreateFiltersGroupDTO filtersGroup) => new FiltersGroup
        {
            CategoryId = filtersGroup.CategoryId,
            SpecificationTypes = filtersGroup.SpecificationTypes.FromDTO().ToArray()
        };

        public static FiltersGroup FromDTO(this UpdateFiltersGroupDTO filtersGroup) => new FiltersGroup
        {
            Id = filtersGroup.Id,
            CategoryId = filtersGroup.CategoryId,
            Category = filtersGroup.Category?.FromDTO(),
            SpecificationTypes = filtersGroup.SpecificationTypes.FromDTO().ToArray()
        };

        public static IEnumerable<FiltersGroupDTO> ToDTO(this IEnumerable<FiltersGroup> filtersGroup) => filtersGroup.Select(f => f.ToDTO());
    }
}
