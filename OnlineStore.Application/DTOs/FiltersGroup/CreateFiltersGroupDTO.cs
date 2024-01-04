using OnlineStore.Application.DTOs.SpecificationType;

namespace OnlineStore.Application.DTOs.FiltersGroup
{
    public class CreateFiltersGroupDTO
    {
        public int CategoryId { get; set; }

        public ICollection<SpecificationTypeDTO> SpecificationTypes { get; set; } = new HashSet<SpecificationTypeDTO>();
    }
}
