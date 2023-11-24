using OnlineStore.Application.DTOs.Base;

namespace OnlineStore.Application.DTOs.Category
{
    public class UpdateCategoryDTO : BaseDTO
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int? RootId { get; set; }

        public int? ParentId { get; set; }

        public bool IsMainCategory { get; set; }
    }
}
