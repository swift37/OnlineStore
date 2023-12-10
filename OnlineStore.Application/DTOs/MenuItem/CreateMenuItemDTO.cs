using OnlineStore.Application.DTOs.Category;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.DTOs.MenuItem
{
    public class CreateMenuItemDTO
    {
        public string? Name { get; set; }

        public int CategoryId { get; set; }

        public bool IsMegaMenu { get; set; }

        public ICollection<CreateNestedMenuItemDTO> NestedItems { get; set; } = new HashSet<CreateNestedMenuItemDTO>();

        public string? Image {  get; set; }
    }

    public class CreateNestedMenuItemDTO
    {
        public string? Name { get; set; }

        public int ParentId { get; set; }

        public ICollection<CategoryDTO> Categories { get; set; } = new HashSet<CategoryDTO>();
    }
}
