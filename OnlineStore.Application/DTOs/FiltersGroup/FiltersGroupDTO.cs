using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Category;

namespace OnlineStore.Application.DTOs.FiltersGroup
{
    public class FiltersGroupDTO : BaseDTO
    {
        public int CategoryId { get; set; }

        public CategoryDTO? Category { get; set; }

        public IDictionary<string, FilterDTO> Filters { get; set; } = new Dictionary<string, FilterDTO>();
    }
}
