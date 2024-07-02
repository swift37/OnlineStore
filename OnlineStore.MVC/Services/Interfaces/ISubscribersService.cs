using OnlineStore.MVC.Models.Subscriber;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface ISubscribersService
    {
        Task<Response<IEnumerable<SubscriberViewModel>>> GetAll();

        Task<Response<bool>> Exist(int id);

        Task<Response<SubscriberViewModel>> Get(int id);

        Task<Response<int>> Create(CreateSubscriberViewModel createSubscriberViewModel);

        Task<Response> Update(SubscriberViewModel subscriberViewModel);

        Task<Response> Delete(int id);

        Task<Response<SubscriberViewModel>> Get(string email);
    }
}
