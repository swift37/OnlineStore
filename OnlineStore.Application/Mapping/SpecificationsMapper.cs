using OnlineStore.Application.DTOs;
using OnlineStore.Domain;

namespace OnlineStore.Application.Mapping
{
    public static class SpecificationsMapper
    {
        public static SpecificationDTO ToDTO(this Specification specification) => new SpecificationDTO
        {
            Id = specification.Id,
            Name = specification.Name,
            Value = specification.Value,
            IsMain = specification.IsMain
        };

        public static Specification FromDTO(this SpecificationDTO specification) => new Specification
        {
            Id = specification.Id,
            Name = specification.Name,
            Value = specification.Value,
            IsMain = specification.IsMain
        };

        public static IEnumerable<SpecificationDTO> ToDTO(this IEnumerable<Specification> specifications) => specifications.Select(s => s.ToDTO());

        public static IEnumerable<Specification> FromDTO(this IEnumerable<SpecificationDTO> specifications) => specifications.Select(s => s.FromDTO());
    }
}
