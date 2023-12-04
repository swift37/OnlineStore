using OnlineStore.MVC.Models.Product;

namespace OnlineStore.MVC.Models.Review
{
    public class ReviewViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public ProductViewModel? Product { get; set; }

        public DateTimeOffset CreationDate { get; set; }

        public string? Title { get; set; }

        public int Rating { get; set; }

        public string? Content { get; set; }
    }
}
