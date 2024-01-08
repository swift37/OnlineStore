using OnlineStore.MVC.Models.Enums;

namespace OnlineStore.MVC.Models
{
    public class ProductsFilteringOptions
    {
        public int CategoryId { get; set; }

        public int MinPrice { get; set; }

        public int MaxPrice { get; set; }

        public ICollection<int> Specifications { get; set; } = new HashSet<int>();

        public int PageNumber { get; set; }

        public int ItemsPerPage { get; set; }

        public SortParameter SortBy { get; set; }
    }
}
