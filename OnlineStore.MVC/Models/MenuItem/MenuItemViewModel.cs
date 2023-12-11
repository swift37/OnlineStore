using OnlineStore.MVC.Models.Category;

namespace OnlineStore.MVC.Models.MenuItem
{
    public class MenuItemViewModel
    {
        public int Id { get; set; }

        public string? NavigationLabel {  get; set; } 

        public int CategoryId { get; set; }

        public CategoryViewModel? Category { get; set; }

        public bool IsMegaMenu { get; set; }

        public ICollection<NestedMenuItemViewModel> NestedItems { get; set; } = new HashSet<NestedMenuItemViewModel>();

        public string? Image { get; set; }
    }

    public class NestedMenuItemViewModel
    {
        public int Id { get; set; }

        public string? NavigationLabel { get; set; }

        public int ParentId { get; set; }

        public MenuItemViewModel? Parent { get; set; }

        public ICollection<CategoryViewModel> Categories { get; set; } = new HashSet<CategoryViewModel>();

        public bool HasTwoColumns { get; set; }
    }
}
