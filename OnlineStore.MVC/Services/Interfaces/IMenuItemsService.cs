using OnlineStore.MVC.Models.MenuItem;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface IMenuItemsService
    {
        Task<Response<IEnumerable<MenuItemViewModel>>> GetAll();

        Task<Response<bool>> Exist(int id);

        Task<Response<MenuItemViewModel>> Get(int id);

        Task<Response<int>> Create(CreateMenuItemViewModel createMenuItemViewModel);

        Task<Response> Update(MenuItemViewModel menuItemViewModel);

        Task<Response> Delete(int id);
    }
}
