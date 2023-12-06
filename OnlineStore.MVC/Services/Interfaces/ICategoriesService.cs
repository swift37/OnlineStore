using OnlineStore.MVC.Models.Category;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task<Response<IEnumerable<CategoryViewModel>>> GetAll();

        Task<Response<bool>> Exist(int id);

        Task<Response<CategoryViewModel>> Get(int id);

        Task<Response<int>> Create(CreateCategoryViewModel createCategoryViewModel);

        Task<Response> Update(CategoryViewModel categoryViewModel);

        Task<Response> Delete(int id);
    }
}
