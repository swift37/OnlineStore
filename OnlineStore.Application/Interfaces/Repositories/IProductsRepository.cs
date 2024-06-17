using OnlineStore.Domain;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Enums;

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

        Task<IEnumerable<Product>> GetAllByTagAsync(
            int tagId,
            CancellationToken cancellation = default);

        Task<IEnumerable<Product>> GetAllByTagAsync(
            string tagName,
            CancellationToken cancellation = default);

        Task<IEnumerable<Product>> GetAllByAvailabilityAsync(
            ProductAvailability productAvailability,
            CancellationToken cancellation = default);

        Task<IEnumerable<Product>> GetAllByStatusAsync(
            ProductStatus productStatus,
            CancellationToken cancellation = default);
    }
}
