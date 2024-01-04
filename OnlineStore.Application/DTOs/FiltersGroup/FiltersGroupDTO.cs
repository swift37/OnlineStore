using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Category;
using OnlineStore.Application.DTOs.SpecificationType;

namespace OnlineStore.Application.DTOs.FiltersGroup
{
    public class FiltersGroupDTO : BaseDTO
    {
        public int CategoryId { get; set; }

        public CategoryDTO? Category { get; set; }

        public ICollection<SpecificationTypeDTO> SpecificationTypes { get; set; } = new HashSet<SpecificationTypeDTO>();
    }
}
