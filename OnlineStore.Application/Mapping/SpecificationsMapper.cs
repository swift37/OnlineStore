using OnlineStore.Application.DTOs.Specification;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Mapping
{
    public static class SpecificationsMapper
    {
        public static SpecificationDTO ToDTO(this Specification specification) => new SpecificationDTO
        {
            Id = specification.Id,
            Value = specification.Value,
            SpecificationTypeId = specification.SpecificationTypeId,
            SpecificationType = specification.SpecificationType?.ToDTO(),
            Products = specification.Products.ToDTO().ToArray()
        };

        public static Specification FromDTO(this SpecificationDTO specification) => new Specification
        {
            Id = specification.Id,
            Value = specification.Value,
            SpecificationTypeId = specification.SpecificationTypeId,
            SpecificationType = specification.SpecificationType?.FromDTO(),
            Products = specification.Products.FromDTO().ToArray()
        };

        public static Specification FromDTO(this CreateSpecificationDTO specification) => new Specification
        {
            Value = specification.Value,
            SpecificationTypeId = specification.SpecificationTypeId,
            Products = specification.Products.FromDTO().ToArray()
        };

        public static Specification FromDTO(this UpdateSpecificationDTO specification) => new Specification
        {
            Id = specification.Id,
            Value = specification.Value,
            SpecificationTypeId = specification.SpecificationTypeId,
            Products = specification.Products.FromDTO().ToArray()
        };

        public static IEnumerable<SpecificationDTO> ToDTO(this IEnumerable<Specification> specifications) => specifications.Select(s => s.ToDTO());

        public static IEnumerable<Specification> FromDTO(this IEnumerable<SpecificationDTO> specifications) => specifications.Select(s => s.FromDTO());

        public static IEnumerable<Specification> FromDTO(this IEnumerable<CreateSpecificationDTO> specifications) => specifications.Select(s => s.FromDTO());

        public static IEnumerable<Specification> FromDTO(this IEnumerable<UpdateSpecificationDTO> specifications) => specifications.Select(s => s.FromDTO());
    }
}
