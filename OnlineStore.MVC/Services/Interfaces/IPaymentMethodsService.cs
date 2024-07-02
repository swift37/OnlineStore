using OnlineStore.MVC.Models.PaymentMethod;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface IPaymentMethodsService
    {
        Task<Response<IEnumerable<PaymentMethodViewModel>>> GetAll();

        Task<Response<bool>> Exist(int id);

        Task<Response<PaymentMethodViewModel>> Get(int id);

        Task<Response<int>> Create(CreatePaymentMethodViewModel createPaymentMethodViewModel);

        Task<Response> Update(PaymentMethodViewModel paymentMethodViewModel);

        Task<Response> Delete(int id);
    }
}
