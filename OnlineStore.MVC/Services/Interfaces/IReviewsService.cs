using OnlineStore.MVC.Models.Review;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface IReviewsService
    {
        Task<Response<IEnumerable<ReviewViewModel>>> GetAll();

        Task<Response<bool>> Exist(int id);

        Task<Response<ReviewViewModel>> Get(int id);

        Task<Response<int>> Create(CreateReviewViewModel createReviewViewModel);

        Task<Response> Update(ReviewViewModel reviewViewModel);

        Task<Response> Delete(int id);

        Task<Response<IEnumerable<ReviewViewModel>>> GetReviewsByProduct(int productId);
    }
}
