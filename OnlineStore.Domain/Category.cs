using OnlineStore.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Domain
{
    public class Category : NamedEntity
    {
        public string? Description { get; set; }

        public Category? Parent { get; set; }

        public IEnumerable<Category?> Subcategories { get; set; } = Enumerable.Empty<Category?>();

        public IEnumerable<Product?> Products { get; set; } = Enumerable.Empty<Product?>();

        public bool IsMainCategory { get; set; }

        [NotMapped]
        public bool IsActive => Products?.Any() ?? false;
    }
}
