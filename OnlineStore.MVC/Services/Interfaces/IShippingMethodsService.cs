using OnlineStore.MVC.Models.ShippingMethod;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface IShippingMethodsService
    {
        Task<Response<IEnumerable<ShippingMethodViewModel>>> GetAll();

        Task<Response<bool>> Exist(int id);

        Task<Response<ShippingMethodViewModel>> Get(int id);

        Task<Response<int>> Create(CreateShippingMethodViewModel createShippingMethodViewModel);

        Task<Response> Update(ShippingMethodViewModel shippingMethodViewModel);

        Task<Response> Delete(int id);
    }
}
