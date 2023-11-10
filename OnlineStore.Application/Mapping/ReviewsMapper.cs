using OnlineStore.Application.Models;
using OnlineStore.Domain;

namespace OnlineStore.Application.Mapping
{
    public static class ReviewsMapper
    {
        public static ReviewDTO? ToDTO(this Review review) => review is null ? null : new ReviewDTO
        {
            Title = review.Title,
            Product = review.Product?.ToDTO(),
            Content = review.Content,
            Rating = review.Rating,
            CreationDate = review.CreationDate
        };

        public static Review? FromDTO(this ReviewDTO review) => review is null ? null : new Review
        {
            Title = review.Title,
            Product = review.Product?.FromDTO(),
            Content = review.Content,
            Rating = review.Rating,
            CreationDate = review.CreationDate
        };

        public static IEnumerable<ReviewDTO?> ToDTO(this IEnumerable<Review?> reviews) => reviews.Select(s => s?.ToDTO());

        public static IEnumerable<Review?> FromDTO(this IEnumerable<ReviewDTO?> reviews) => reviews.Select(s => s?.FromDTO());
    }
}
