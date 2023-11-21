namespace OnlineStore.Application.DTOs.Category
{
    public class CreateCategoryDTO
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int? RootId { get; set; }

        public int? ParentId { get; set; }

        public bool IsMainCategory { get; set; }
    }
}
