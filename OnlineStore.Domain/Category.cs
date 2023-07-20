using OnlineStore.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Domain
{
    public class Category : NamedEntity
    {
        public string? Description { get; set; }

        [NotMapped]
        public bool IsActive => Products?.Any() ?? false;

        public IEnumerable<SubCategory>? SubCategories { get; set; }

        public IEnumerable<Product>? Products { get; set; }
    }
}
