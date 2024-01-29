using OnlineStore.MVC.Models.Category;
using OnlineStore.MVC.Models.MenuItem;

namespace OnlineStore.MVC.Models.NestedMenuItem
{
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
