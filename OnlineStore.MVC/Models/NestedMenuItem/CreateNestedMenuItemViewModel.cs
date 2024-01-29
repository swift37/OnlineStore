namespace OnlineStore.MVC.Models.NestedMenuItem
{
    public class CreateNestedMenuItemViewModel
    {
        public string? NavigationLabel { get; set; }

        public int ParentId { get; set; }

        public ICollection<int> CategoryIds { get; set; } = new HashSet<int>();

        public bool HasTwoColumns { get; set; }
    }
}
