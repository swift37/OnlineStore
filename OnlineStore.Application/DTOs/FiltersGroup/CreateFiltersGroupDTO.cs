using OnlineStore.Application.DTOs.Product;

namespace OnlineStore.Application.DTOs.FiltersGroup
{
    public class CreateFiltersGroupDTO
    {
        public int CategoryId { get; set; }

        public ICollection<SpecificationDTO> Specifications { get; set; } = new HashSet<SpecificationDTO>();
    }
}
