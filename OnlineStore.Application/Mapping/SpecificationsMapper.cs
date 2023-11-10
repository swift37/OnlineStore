using OnlineStore.Application.Models;
using OnlineStore.Domain;

namespace OnlineStore.Application.Mapping
{
    public static class SpecificationsMapper
    {
        public static SpecificationDTO? ToDTO(this Specification specification) => specification is null ? null : new SpecificationDTO
        {
            Name = specification.Name,
            Value = specification.Value,
            IsMain = specification.IsMain
        };

        public static Specification? FromDTO(this SpecificationDTO specification) => specification is null ? null : new Specification
        {
            Name = specification.Name,
            Value = specification.Value,
            IsMain = specification.IsMain
        };

        public static IEnumerable<SpecificationDTO?> ToDTO(this IEnumerable<Specification?> specifications) => specifications.Select(s => s?.ToDTO());

        public static IEnumerable<Specification?> FromDTO(this IEnumerable<SpecificationDTO?> specifications) => specifications.Select(s => s?.FromDTO());
    }
}
