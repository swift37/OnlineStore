using OnlineStore.Domain;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Enums;

namespace OnlineStore.Application.Interfaces.Repositories
{
    public interface IProductsRepository : IRepository<Product>
    {
        Task<ProductsPage> GetProductsByCategoryAsync(
            int catId,
            int page = 1,
            int itemsPerPage = 15,
            SortParameters sortBy = SortParameters.Default,
            CancellationToken cancellation = default);
    }
}
