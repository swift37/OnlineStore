using OnlineStore.Domain;

namespace OnlineStore.Application.Interfaces.Repositories
{
    public interface IReviewsRepository : IRepository<Review>
    {
        Task<Review?> GetReviewsByProductAsync(
            int productId,
            CancellationToken cancellation = default);
    }
}
