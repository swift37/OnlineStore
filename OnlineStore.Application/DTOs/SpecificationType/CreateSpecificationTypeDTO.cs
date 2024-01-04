using OnlineStore.Application.DTOs.Specification;

namespace OnlineStore.Application.DTOs.SpecificationType
{
    public class CreateSpecificationTypeDTO
    {
        public string? Name { get; set; }

        public string? DisplayName { get; set; }

        public bool IsMain { get; set; }

        public ICollection<CreateSpecificationDTO> Values { get; set; } = new HashSet<CreateSpecificationDTO>();
    }
}
