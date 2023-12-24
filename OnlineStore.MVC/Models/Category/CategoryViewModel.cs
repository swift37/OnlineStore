namespace OnlineStore.MVC.Models.Category
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int? RootId { get; set; }

        public int? ParentId { get; set; }

        public ICollection<CategoryViewModel> ChildCategories { get; set; } = new HashSet<CategoryViewModel>();

        public bool IsMainCategory { get; set; }

        public bool HasChildCategories => ChildCategories.Count > 0;
    }
}
