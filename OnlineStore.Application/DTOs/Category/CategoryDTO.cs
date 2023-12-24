using OnlineStore.Application.DTOs.Base;

namespace OnlineStore.Application.DTOs.Category
{
    public class CategoryDTO : BaseDTO
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int? RootId { get; set; }

        public int? ParentId { get; set; }

        public ICollection<CategoryDTO> ChildCategories { get; set; } = new HashSet<CategoryDTO>();

        public bool IsMainCategory { get; set; }
    }
}
