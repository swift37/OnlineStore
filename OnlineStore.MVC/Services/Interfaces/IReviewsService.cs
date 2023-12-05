using OnlineStore.MVC.Models.Review;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface IReviewsService
    {
        Task<IEnumerable<ReviewViewModel>> GetAll();

        Task<bool> Exist(int id);

        Task<ReviewViewModel> Get(int id);

        Task<Response<int>> Create(CreateReviewViewModel createReviewViewModel);

        Task<Response> Update(ReviewViewModel reviewViewModel);

        Task<Response> Delete(int id);

        Task<IEnumerable<ReviewViewModel>> GetReviewsByProduct(int productId);
    }
}
