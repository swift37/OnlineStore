using AutoMapper;
using OnlineStore.MVC.Models.Coupon;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Services
{
    public class CouponsService : HttpClientServiceBase, ICouponsService
    {
        public CouponsService(IMapper mapper, IClient client) : base(mapper, client) { }

        public Task<IEnumerable<CouponViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CouponViewModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exist(int id)
        {
            throw new NotImplementedException();
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
