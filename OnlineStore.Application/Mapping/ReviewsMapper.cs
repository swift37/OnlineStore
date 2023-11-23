using OnlineStore.Application.DTOs.Review;
using OnlineStore.Domain;

namespace OnlineStore.Application.Mapping
{
    public static class ReviewsMapper
    {
        public static ReviewDTO ToDTO(this Review review) => new ReviewDTO
        {
            Id = review.Id,
            Title = review.Title,
            ProductId = review.ProductId,
            Product = review.Product?.ToDTO(),
            Content = review.Content,
            Rating = review.Rating,
            CreationDate = review.CreationDate
        };

        public static Review FromDTO(this ReviewDTO review) => new Review
        {
            Id = review.Id,
            Title = review.Title,
            ProductId = review.ProductId,
            Product = review.Product?.FromDTO(),
            Content = review.Content,
            Rating = review.Rating,
            CreationDate = review.CreationDate
        };

        public static Review FromDTO(this CreateReviewDTO review) => new Review
        {
            Title = review.Title,
            ProductId = review.ProductId,
            Content = review.Content,
            Rating = review.Rating,
            CreationDate = review.CreationDate
        };

        public static Review FromDTO(this UpdateReviewDTO review) => new Review
        {
            Id = review.Id,
            Title = review.Title,
            Content = review.Content,
            Rating = review.Rating
        };

        public static IEnumerable<ReviewDTO> ToDTO(this IEnumerable<Review> reviews) => reviews.Select(s => s.ToDTO());

        public static IEnumerable<Review> FromDTO(this IEnumerable<ReviewDTO> reviews) => reviews.Select(s => s.FromDTO());
    }
}
