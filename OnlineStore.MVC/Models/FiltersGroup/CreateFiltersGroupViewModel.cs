using OnlineStore.MVC.Services.ApiClient;

namespace OnlineStore.MVC.Models.FiltersGroup
{
    public class CreateFiltersGroupViewModel
    {
        public int CategoryId { get; set; }

        public ICollection<SpecificationDTO> Specifications { get; set; } = new HashSet<SpecificationDTO>();
    }
}
