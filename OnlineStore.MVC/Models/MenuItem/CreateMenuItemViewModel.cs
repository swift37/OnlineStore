namespace OnlineStore.MVC.Models.MenuItem
{
    public class CreateMenuItemViewModel
    {
        public string? NavigationLabel { get; set; }

        public int CategoryId { get; set; }

        public bool IsMegaMenu { get; set; }

        public string? Image { get; set; }
    }
}
