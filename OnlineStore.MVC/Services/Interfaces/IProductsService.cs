using OnlineStore.MVC.Models;
using OnlineStore.MVC.Models.Product;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface IProductsService
    {
        Task<Response<IEnumerable<ProductViewModel>>> GetAll();

        Task<Response<bool>> Exist(int id);

        Task<Response<ProductViewModel>> Get(int id);

        Task<Response<int>> Create(CreateProductViewModel createProductViewModel);

        Task<Response> Update(ProductViewModel productViewModel);

        Task<Response> Delete(int id);

        Task<Response<ProductsPageViewModel>> GetFilteredProducts(ProductsFilteringOptions options);
    }
}
