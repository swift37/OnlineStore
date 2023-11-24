using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Models.ViewModels
{

    public class SubCategoryViewModel : Category
    {
        public IEnumerable<SelectListItem> AvailableCategories { get; set; } = new HashSet<SelectListItem>();
    }
}
