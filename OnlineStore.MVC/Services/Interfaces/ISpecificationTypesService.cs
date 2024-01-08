using OnlineStore.MVC.Models.SpecificationType;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface ISpecificationTypesService
    {
        Task<Response<IEnumerable<SpecificationTypeViewModel>>> GetAll();

        Task<Response<bool>> Exist(int id);

        Task<Response<SpecificationTypeViewModel>> Get(int id);

        Task<Response<int>> Create(CreateSpecificationTypeViewModel createSpecificationTypeViewModel);

        Task<Response> Update(SpecificationTypeViewModel specificationTypeViewModel);

        Task<Response> Delete(int id);
    }
}
