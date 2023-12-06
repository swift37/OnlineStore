using AutoMapper;
using OnlineStore.MVC.Models.Coupon;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Services
{
    public class CouponsService : HttpClientServiceBase, ICouponsService
    {
        public CouponsService(IMapper mapper, IClient client, IHttpContextAccessor httpContextAccessor)
            : base(mapper, client, httpContextAccessor) { }

        public async Task<Response<IEnumerable<CouponViewModel>>> GetAll()
        {
            try
            {
                var categories = await _client.GetAllCouponsAsync(_usingVersion);
                return new Response<IEnumerable<CouponViewModel>>
                {
                    Success = true,
                    Data = _mapper.Map<IEnumerable<CouponViewModel>>(categories)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<IEnumerable<CouponViewModel>>(exception);
            }
        }

        public async Task<Response<CouponViewModel>> Get(int id)
        {
            try
            {
                var category = await _client.GetCouponAsync(id, _usingVersion);
                return new Response<CouponViewModel>
                {
                    Success = true,
                    Data = _mapper.Map<CouponViewModel>(category)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<CouponViewModel>(exception);
            }
        }

        public async Task<Response<bool>> Exist(int id)
        {
            try
            {
                var category = await _client.ExistCouponAsync(id, _usingVersion);
                return new Response<bool>
                {
                    Success = true,
                    Data = _mapper.Map<bool>(category)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<bool>(exception);
            }
        }

        public async Task<Response<int>> Create(CreateCouponViewModel createCouponViewModel)
        {
            var createCouponDTO = _mapper.Map<CreateCouponDTO>(createCouponViewModel);

            try
            {
                var response = await _client.CreateCouponAsync(_usingVersion, createCouponDTO);
                return new Response<int>
                {
                    Success = true,
                    Data = response
                };
            }
            catch (ApiException e)
            {
                var generatedResponse = GenerateResponse(e);
                return new Response<int>(generatedResponse);
            }
        }

        public async Task<Response> Update(CouponViewModel couponViewModel)
        {
            var updateCouponDTO = _mapper.Map<UpdateCouponDTO>(couponViewModel);

            try
            {
                await _client.UpdateCouponAsync(_usingVersion, updateCouponDTO);
                return new Response { Success = true };
            }
            catch (ApiException e)
            {
                return GenerateResponse(e);
            }
        }

        public async Task<Response> Delete(int id)
        {
            try
            {
                await _client.DeleteCouponAsync(id, _usingVersion);
                return new Response { Success = true };
            }
            catch (ApiException e)
            {
                return GenerateResponse(e);
            }
        }
    }
}
