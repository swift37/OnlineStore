namespace OnlineStore.MVC.Models.Category
{
    public class CreateCategoryViewModel
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int? RootId { get; set; }

        public int? ParentId { get; set; }

        public bool IsMainCategory { get; set; }
    }
}
