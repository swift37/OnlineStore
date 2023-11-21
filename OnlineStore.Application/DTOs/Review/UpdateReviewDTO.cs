using OnlineStore.Application.DTOs.Base;

namespace OnlineStore.Application.DTOs.Review
{
    public class UpdateReviewDTO : BaseDTO
    {
        public string? Title { get; set; }

        public double Rating { get; set; }

        public string? Content { get; set; }
    }
}
