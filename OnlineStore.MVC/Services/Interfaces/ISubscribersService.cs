using OnlineStore.MVC.Models.Subscriber;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface ISubscribersService
    {
        Task<IEnumerable<SubscriberViewModel>> GetAll();

        Task<bool> Exist(int id);

        Task<SubscriberViewModel> Get(int id);

        Task<Response<int>> Create(CreateSubscriberViewModel createSubscriberViewModel);

        Task<Response> Update(SubscriberViewModel subscriberViewModel);

        Task<Response> Delete(int id);
    }
}
