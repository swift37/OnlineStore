using OnlineStore.MVC.Models.ProductTag;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface IProductTagsService
    {
        Task<Response<IEnumerable<ProductTagViewModel>>> GetAll();

        Task<Response<bool>> Exist(int id);

        Task<Response<ProductTagViewModel>> Get(int id);

        Task<Response<int>> Create(CreateProductTagViewModel createProductTagViewModel);

        Task<Response> Update(ProductTagViewModel productTagViewModel);

        Task<Response> Delete(int id);
    }
}
