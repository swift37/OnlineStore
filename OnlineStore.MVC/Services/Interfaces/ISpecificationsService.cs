using OnlineStore.MVC.Models.Specification;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface ISpecificationsService
    {
        Task<Response<IEnumerable<SpecificationViewModel>>> GetAll();

        Task<Response<bool>> Exist(int id);

        Task<Response<SpecificationViewModel>> Get(int id);

        Task<Response<int>> Create(CreateSpecificationViewModel createSpecificationViewModel);

        Task<Response> Update(SpecificationViewModel specificationViewModel);

        Task<Response> Delete(int id);
    }
}
