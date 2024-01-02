using OnlineStore.Domain;

namespace OnlineStore.Application.Interfaces.Repositories
{
    public interface IProductsRepository : IRepository<Product>
    {
        Task<ProductsPage> GetProductsByCategoryAsync(
            int catId,
            int page = 1,
            int itemsPerPage = 15,
            CancellationToken cancellation = default);
    }
}
