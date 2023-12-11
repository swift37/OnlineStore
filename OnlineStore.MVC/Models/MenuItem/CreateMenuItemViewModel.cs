using OnlineStore.MVC.Models.Category;

namespace OnlineStore.MVC.Models.MenuItem
{
    public class CreateMenuItemViewModel
    {
        public string? NavigationLabel { get; set; }

        public int CategoryId { get; set; }

        public bool IsMegaMenu { get; set; }

        public ICollection<CreateNestedMenuItemViewModel> NestedItems { get; set; } = new HashSet<CreateNestedMenuItemViewModel>();

        public string? Image { get; set; }
    }

    public class CreateNestedMenuItemViewModel
    {
        public string? NavigationLabel { get; set; }

        public int ParentId { get; set; }

        public ICollection<CategoryViewModel> Categories { get; set; } = new HashSet<CategoryViewModel>();

        public bool HasTwoColumns { get; set; }
    }
}
