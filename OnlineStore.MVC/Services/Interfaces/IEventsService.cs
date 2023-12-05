using OnlineStore.MVC.Models.Event;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface IEventsService
    {
        Task<IEnumerable<EventViewModel>> GetAll();

        Task<bool> Exist(int id);

        Task<EventViewModel> Get(int id);

        Task<Response<int>> Create(CreateEventViewModel createEventViewModel);

        Task<Response> Update(EventViewModel eventViewModel);

        Task<Response> Delete(int id);
    }
}
