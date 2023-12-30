using Microsoft.EntityFrameworkCore;
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
    }
}
