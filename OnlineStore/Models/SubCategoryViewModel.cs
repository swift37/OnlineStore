using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineStore.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models
{
    
    public class SubCategoryViewModel : SubCategory
    {
        public IEnumerable<SelectListItem> AvailableCategories { get; set; } = new HashSet<SelectListItem>();
    }
}
