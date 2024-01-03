using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Category;
using OnlineStore.Application.DTOs.Product;

namespace OnlineStore.Application.DTOs.FiltersGroup
{
    public class UpdateFiltersGroupDTO : BaseDTO
    {
        public int CategoryId { get; set; }

        public CategoryDTO? Category { get; set; }

        public ICollection<SpecificationDTO> Specifications { get; set; } = new HashSet<SpecificationDTO>();
    }
}
