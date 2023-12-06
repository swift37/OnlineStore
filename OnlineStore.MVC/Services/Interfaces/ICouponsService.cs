using OnlineStore.MVC.Models.Coupon;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface ICouponsService
    {
        Task<Response<IEnumerable<CouponViewModel>>> GetAll();

        Task<Response<bool>> Exist(int id);

        Task<Response<CouponViewModel>> Get(int id);

        Task<Response<int>> Create(CreateCouponViewModel createCouponViewModel);

        Task<Response> Update(CouponViewModel couponViewModel);

        Task<Response> Delete(int id);
    }
}
