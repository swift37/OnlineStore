using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Specification;

namespace OnlineStore.Application.DTOs.SpecificationType
{
    public class SpecificationTypeDTO : BaseDTO
    {
        public string? Name { get; set; }

        public string? DisplayName { get; set; }

        public bool IsMain { get; set; }

        public ICollection<SpecificationDTO> Values { get; set; } = new HashSet<SpecificationDTO>();
    }
}
