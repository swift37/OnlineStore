using OnlineStore.MVC.Models;
using OnlineStore.MVC.Models.FiltersGroup;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface IFilterGroupsService
    {
        Task<Response<IEnumerable<FiltersGroupViewModel>>> GetAll();

        Task<Response<bool>> Exist(int id);

        Task<Response<FiltersGroupViewModel>> Get(int id);

        Task<Response<int>> Create(CreateFiltersGroupViewModel createFiltersGroupViewModel);

        Task<Response> Update(UpdateFiltersGroupViewModel updateFiltersGroupViewModel);

        Task<Response> Delete(int id);

        Task<Response<FiltersGroupViewModel>> GetCategoryFiltersGroup(int categoryId);

        Task<Response<FiltersGroupViewModel>> GetCategoryFiltersGroup(FiltersGroupOptions filtersGroupOptions);
    }
}
