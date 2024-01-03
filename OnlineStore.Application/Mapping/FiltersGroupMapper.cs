using OnlineStore.Application.DTOs.FiltersGroup;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Mapping
{
    public static class FiltersGroupMapper
    {
        public static FiltersGroupDTO ToDTO(this FiltersGroup filtersGroup)
        {
            var filtersGroupDTO = new FiltersGroupDTO
            {
                Id = filtersGroup.Id,
                CategoryId = filtersGroup.CategoryId,
                Category = filtersGroup.Category?.ToDTO()
            };

            foreach (var spec in filtersGroup.Specifications)
                if (!string.IsNullOrEmpty(spec.Name))
                    filtersGroupDTO.Filters.Add(spec.Name, new FilterDTO
                    {
                        Value = spec.Value,
                    });

            return filtersGroupDTO;
        }

        public static FiltersGroup FromDTO(this CreateFiltersGroupDTO filtersGroup) => new FiltersGroup
        {
            CategoryId = filtersGroup.CategoryId,
            Specifications = filtersGroup.Specifications.FromDTO().ToArray()
        };

        public static FiltersGroup FromDTO(this UpdateFiltersGroupDTO filtersGroup) => new FiltersGroup
        {
            Id = filtersGroup.Id,
            CategoryId = filtersGroup.CategoryId,
            Category = filtersGroup.Category?.FromDTO(),
            Specifications = filtersGroup.Specifications.FromDTO().ToArray()
        };

        public static IEnumerable<FiltersGroupDTO> ToDTO(this IEnumerable<FiltersGroup> filtersGroup) => filtersGroup.Select(f => f.ToDTO());
    }
}
