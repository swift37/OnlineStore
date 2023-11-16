using OnlineStore.Domain;

namespace OnlineStore.Application.Interfaces.Repositories
{
    public interface IProductsRepository : IRepository<Product>
    {
        Task<ProductsPage?> GetProductByCategory(
            int catId,
            int page = 1,
            int itemsPerPage = 15,
            SortParameters sortBy = SortParameters.Default,
            CancellationToken cancellation = default);

        enum SortParameters
        {
            Default,
            RatingDescending,
            PriceAscending,
            PriceDescending
        }
    }
}
