using OnlineStore.Application.DTOs.SpecificationType;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Mapping
{
    public static class SpecificationTypesMapper
    {
        public static SpecificationTypeDTO ToDTO(this SpecificationType specification) => new SpecificationTypeDTO
        {
            Id = specification.Id,
            Name = specification.Name,
            DisplayName = specification.DisplayName,
            IsMain = specification.IsMain,
            Values = specification.Values.ToDTO().ToArray()
        };

        public static SpecificationType FromDTO(this SpecificationTypeDTO specification) => new SpecificationType
        {
            Id = specification.Id,
            Name = specification.Name,
            DisplayName = specification.DisplayName,
            IsMain = specification.IsMain,
            Values = specification.Values.FromDTO().ToArray()
        };

        public static SpecificationType FromDTO(this CreateSpecificationTypeDTO specification) => new SpecificationType
        {
            Name = specification.Name,
            DisplayName = specification.DisplayName,
            IsMain = specification.IsMain,
            Values = specification.Values.FromDTO().ToArray()
        };

        public static SpecificationType FromDTO(this UpdateSpecificationTypeDTO specification) => new SpecificationType
        {
            Id = specification.Id,
            Name = specification.Name,
            DisplayName= specification.DisplayName,
            IsMain = specification.IsMain,
            Values = specification.Values.FromDTO().ToArray()
        };

        public static IEnumerable<SpecificationTypeDTO> ToDTO(this IEnumerable<SpecificationType> specifications) => specifications.Select(s => s.ToDTO());

        public static IEnumerable<SpecificationType> FromDTO(this IEnumerable<SpecificationTypeDTO> specifications) => specifications.Select(s => s.FromDTO());

        public static IEnumerable<SpecificationType> FromDTO(this IEnumerable<CreateSpecificationTypeDTO> specifications) => specifications.Select(s => s.FromDTO());

        public static IEnumerable<SpecificationType> FromDTO(this IEnumerable<UpdateSpecificationTypeDTO> specifications) => specifications.Select(s => s.FromDTO());
    }
}
