using OnlineStore.MVC.Models.ContactRequest;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface IContactRequestsService
    {
        Task<IEnumerable<ContactRequestViewModel>> GetAll();

        Task<bool> Exist(int id);

        Task<ContactRequestViewModel> Get(int id);

        Task<Response<int>> Create(CreateContactRequestViewModel createContactRequestViewModel);

        Task<Response> Update(ContactRequestViewModel contactRequestViewModel);

        Task<Response> Delete(int id);
    }
}
