using OnlineStore.Domain.Base;

namespace OnlineStore.Domain
{
    public class SubCategory : NamedEntity
    {
        public Category? Category { get; set; }

        public IEnumerable<Product>? Products { get; set; }
    }
}
