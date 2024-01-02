using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Enums;

namespace OnlineStore.Domain
{
    public class ProductsFilteringOptions
    {
        public int CategoryId { get; set; }

        public int MinPrice { get; set; }

        public int MaxPrice { get; set; }

        public ICollection<Specification> Specifications { get; set; } = new HashSet<Specification>();

        public int PageNumber {  get; set; }

        public int ItemsPerPage { get; set; }

        public SortParameter SortBy { get; set; }
    }
}
