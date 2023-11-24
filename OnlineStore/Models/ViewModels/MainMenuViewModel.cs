using OnlineStore.Domain.Entities;

namespace OnlineStore.Models.ViewModels
{
    public class MainMenuViewModel
    {
        public ICollection<Category> Categories { get; set; } = new HashSet<Category>();
    }
}
