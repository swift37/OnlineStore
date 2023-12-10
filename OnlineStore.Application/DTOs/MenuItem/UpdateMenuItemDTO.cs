using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Category;

namespace OnlineStore.Application.DTOs.MenuItem
{
    public class UpdateMenuItemDTO : BaseDTO
    {
        public string? Name { get; set; }

        public int CategoryId { get; set; }

        public bool IsMegaMenu { get; set; }

        public ICollection<UpdateNestedMenuItemDTO> NestedItems { get; set; } = new HashSet<UpdateNestedMenuItemDTO>();

        public string? Image { get; set; }
    }

    public class UpdateNestedMenuItemDTO : BaseDTO
    {
        public string? Name { get; set; }

        public int ParentId { get; set; }

        public ICollection<CategoryDTO> Categories { get; set; } = new HashSet<CategoryDTO>();
    }
}
