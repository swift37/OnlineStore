using OnlineStore.MVC.Models.Order;
using OnlineStore.MVC.Models.Product;

namespace OnlineStore.MVC.Models.Review
{
    public class ReviewViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public ProductViewModel? Product { get; set; }

        public int? OrderId { get; set; }

        public OrderViewModel? Order { get; set; }

        public DateTimeOffset CreationDate { get; set; }

        public DateTimeOffset LastChangeDate { get; set; }

        public string? Name { get; set; }

        public int Rating { get; set; }

        public string? Content { get; set; }
    }
}
