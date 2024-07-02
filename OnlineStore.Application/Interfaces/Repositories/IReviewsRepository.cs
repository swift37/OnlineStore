using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces.Repositories
{
    public interface IReviewsRepository : IRepository<Review>
    {
        Task<IEnumerable<Review>> GetReviewsByProductAsync(
            int productId,
            CancellationToken cancellation = default);

        Task<int> GetReviewsCountByProductAsync(
            int productId,
            CancellationToken cancellation = default);

        Task<double> GetProductRatingAsync(
            int productId,
            CancellationToken cancellation = default);

        Task<IEnumerable<Review>> GetUserReviewsAsync(
            Guid userId,
            CancellationToken cancellation = default);

        Task<Review> GetUserReviewAsync(
            int id,
            Guid userId,
            CancellationToken cancellation = default);
    }
}
