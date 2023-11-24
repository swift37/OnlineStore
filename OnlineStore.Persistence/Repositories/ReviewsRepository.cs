using Azure;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.DAL.Context;
using OnlineStore.Domain.Entities;
using System.Globalization;

namespace OnlineStore.DAL.Repositories
{
    public class ReviewsRepository : Repository<Review>, IReviewsRepository
    {
        public ReviewsRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Review>> GetReviewsByProductAsync(int productId, CancellationToken cancellation = default)
        {
            var reviews = await Entities
                .Where(r => r.ProductId == productId)
                .ToArrayAsync();

            if (reviews is not { Length: > 0 }) 
                return Enumerable.Empty<Review>();

            return reviews;
        }
    }
}
