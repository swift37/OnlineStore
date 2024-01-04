using OnlineStore.Domain.Enums;

namespace OnlineStore.Domain
{
    public class ProductsFilteringOptions
    {
        public int CategoryId { get; set; }

        public int MinPrice { get; set; }

        public int MaxPrice { get; set; }

        public ICollection<int> SpecificationIds { get; set; } = new HashSet<int>();

        public int PageNumber {  get; set; }

        public int ItemsPerPage { get; set; }

        public SortParameter SortBy { get; set; }
    }
}
