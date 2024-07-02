using OnlineStore.MVC.Models.NestedMenuItem;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface INestedMenuItemsService
    {
        Task<Response<IEnumerable<NestedMenuItemViewModel>>> GetAll();

        Task<Response<bool>> Exist(int id);

        Task<Response<NestedMenuItemViewModel>> Get(int id);

        Task<Response<int>> Create(CreateNestedMenuItemViewModel createNestedMenuItemViewModel);

        Task<Response> Update(UpdateNestedMenuItemViewModel updateMenuItemViewModel);

        Task<Response> Delete(int id);
    }
}
