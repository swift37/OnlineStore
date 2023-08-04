using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineStore.Domain;

namespace OnlineStore.Models.ViewModels
{

    public class SubCategoryViewModel : SubCategory
    {
        public IEnumerable<SelectListItem> AvailableCategories { get; set; } = new HashSet<SelectListItem>();
    }
}
