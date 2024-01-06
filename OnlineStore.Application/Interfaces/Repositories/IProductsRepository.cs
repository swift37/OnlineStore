using OnlineStore.Domain;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces.Repositories
{
    public interface IProductsRepository : IRepository<Product>
    {
        Task<ProductsPage> GetFilteredProductsAsync(
            ProductsFilteringOptions options,
            CancellationToken cancellation = default);

        Task<int> GetCountByFilterAsync(
            int categoryId,
            int specificationId,
            CancellationToken cancellation = default);
    }
}
