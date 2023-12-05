using OnlineStore.MVC.Models.Category;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task<IEnumerable<CategoryViewModel>> GetAll();

        Task<bool> Exist(int id);

        Task<CategoryViewModel> Get(int id);

        Task<Response<int>> Create(CreateCategoryViewModel createCategoryViewModel);

        Task<Response> Update(CategoryViewModel categoryViewModel);

        Task<Response> Delete(int id);
    }
}
