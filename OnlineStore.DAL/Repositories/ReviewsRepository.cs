using Azure;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.DAL.Context;
using OnlineStore.Domain;
using System.Globalization;

namespace OnlineStore.DAL.Repositories
{
    public class ReviewsRepository : Repository<Review>, IReviewsRepository
    {
        public ReviewsRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Review>> GetReviewsByProductAsync(int productId, CancellationToken cancellation = default)
        {
            var product = await _context.Products
                .Include(p => p.Reviews)
                .SingleOrDefaultAsync(p => p.Id == productId, cancellation)
                .ConfigureAwait(false);

            if (product is null || product.Reviews is not { Count: > 0 }) 
                return Enumerable.Empty<Review>();

            return product.Reviews;
        }
    }
}
