using OnlineStore.MVC.Models.ContactRequest;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface IContactRequestsService
    {
        Task<Response<IEnumerable<ContactRequestViewModel>>> GetAll();

        Task<Response<bool>> Exist(int id);

        Task<Response<ContactRequestViewModel>> Get(int id);

        Task<Response<int>> Create(CreateContactRequestViewModel createContactRequestViewModel);

        Task<Response> Update(ContactRequestViewModel contactRequestViewModel);

        Task<Response> Delete(int id);
    }
}
