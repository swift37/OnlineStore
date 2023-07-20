using OnlineStore.Domain.Base;

namespace OnlineStore.Domain
{
    public class Category : NamedEntity
    {
        public string? Description { get; set; }

        public bool IsActive => Products?.Any() ?? false;

        public IEnumerable<SubCategory>? SubCategories { get; set; }

        public IEnumerable<Product>? Products { get; set; }
    }
}
