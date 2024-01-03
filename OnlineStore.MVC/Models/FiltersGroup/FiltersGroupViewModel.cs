using OnlineStore.MVC.Models.Category;
using OnlineStore.MVC.Services.ApiClient;

namespace OnlineStore.MVC.Models.FiltersGroup
{
    public class FiltersGroupViewModel
    {
        public int Id { get; set; } 

        public int CategoryId { get; set; }

        public CategoryViewModel? Category { get; set; }

        public IDictionary<string, FilterDTO> Filters { get; set; } = new Dictionary<string, FilterDTO>(); 
    }
}
