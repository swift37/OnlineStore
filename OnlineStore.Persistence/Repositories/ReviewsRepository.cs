using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Exeptions;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.Repositories
{
    public class ReviewsRepository : Repository<Review>, IReviewsRepository
    {
        public ReviewsRepository(IApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Review>> GetReviewsByProductAsync(int productId, CancellationToken cancellation = default)
        {
            var reviews = await Entities
                .Where(r => r.ProductId == productId)
                .ToArrayAsync(cancellation)
                .ConfigureAwait(false);

            if (reviews is not { Length: > 0 }) 
                return Enumerable.Empty<Review>();

            return reviews;
        }

        public async Task<int> GetReviewsCountByProductAsync(int productId, CancellationToken cancellation = default) => 
            await Entities
                .Where(r => r.ProductId == productId)
                .CountAsync(cancellation)
                .ConfigureAwait(false);

        public async Task<double> GetProductRatingAsync(int productId, CancellationToken cancellation = default) =>
            await Entities
                .Where(r => r.ProductId == productId)
                .Select(r => r.Rating)
                .DefaultIfEmpty()
                .AverageAsync(cancellation)
                .ConfigureAwait(false);

        public async Task<IEnumerable<Review>> GetUserReviewsAsync(
            Guid userId,
            CancellationToken cancellation = default) => await Entities
            .Where(r => r.UserId == userId)
            .Include(r => r.Product)
            .ToArrayAsync(cancellation);

        public async Task<Review> GetUserReviewAsync(
            int id,
            Guid userId,
            CancellationToken cancellation = default) => await Entities
            .Include(r => r.Product)
            .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId, cancellation)
            .ConfigureAwait(false)
            ?? throw new NotFoundException(nameof(Review), id);
    }
}
