namespace OnlineStore.Application.DTOs.Review
{
    public class CreateReviewDTO
    {
        public int ProductId { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string? Title { get; set; }

        public int Rating { get; set; }

        public string? Content { get; set; }
    }
}
