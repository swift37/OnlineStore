using OnlineStore.MVC.Models.Coupon;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface ICouponsService
    {
        Task<IEnumerable<CouponViewModel>> GetAll();

        Task<bool> Exist(int id);

        Task<CouponViewModel> Get(int id);

        Task<Response<int>> Create(CreateCouponViewModel createCouponViewModel);

        Task<Response> Update(CouponViewModel couponViewModel);

        Task<Response> Delete(int id);
    }
}
