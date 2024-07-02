using OnlineStore.MVC.Models.Category;
using OnlineStore.MVC.Models.NestedMenuItem;

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
}
