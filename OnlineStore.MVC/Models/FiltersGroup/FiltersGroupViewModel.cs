using OnlineStore.MVC.Models.Category;

namespace OnlineStore.MVC.Models.FiltersGroup
{
    public class FiltersGroupViewModel
    {
        public int Id { get; set; } 

        public int CategoryId { get; set; }

        public CategoryViewModel? Category { get; set; }

        public IDictionary<string, FilterViewModel> Filters { get; set; } = new Dictionary<string, FilterViewModel>(); 
    }
}
