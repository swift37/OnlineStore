namespace OnlineStore.MVC.Models.Review
{
    public class CreateReviewViewModel
    {
        public int ProductId { get; set; }

        public string? Title { get; set; }

        public int Rating { get; set; }

        public string? Content { get; set; }
    }
}
