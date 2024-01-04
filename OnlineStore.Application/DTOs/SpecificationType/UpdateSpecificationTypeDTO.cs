using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Specification;

namespace OnlineStore.Application.DTOs.SpecificationType
{
    public class UpdateSpecificationTypeDTO : BaseDTO
    {
        public string? Name { get; set; }

        public string? DisplayName { get; set; }

        public bool IsMain { get; set; }

        public ICollection<UpdateSpecificationDTO> Values { get; set; } = new HashSet<UpdateSpecificationDTO>();
    }
}
