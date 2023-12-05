using OnlineStore.MVC.Models.Enums;
using OnlineStore.MVC.Models.Product;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductViewModel>> GetAll();

        Task<bool> Exist(int id);

        Task<ProductViewModel> Get(int id);

        Task<Response<int>> Create(CreateProductViewModel createProductViewModel);

        Task<Response> Update(ProductViewModel productViewModel);

        Task<Response> Delete(int id);

        Task<ProductsPageViewModel> GetProductsByCategory(
            int categoryId,
            int page = 1,
            int itemsPerPage = 15,
            SortParameters sortBy = SortParameters.Default);
    }
}
