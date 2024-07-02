using AutoMapper;
using OnlineStore.MVC.Models.ShippingMethod;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Services
{
    public class ShippingMethodsService : HttpClientServiceBase, IShippingMethodsService
    {
        public ShippingMethodsService(IMapper mapper, IClient client, IHttpContextAccessor httpContextAccessor)
            : base(mapper, client, httpContextAccessor) { }

        public async Task<Response<IEnumerable<ShippingMethodViewModel>>> GetAll()
        {
            try
            {
                var shippingMethods = await _client.GetAllShippingMethodsAsync(_usingVersion);
                return new Response<IEnumerable<ShippingMethodViewModel>>
                {
                    Success = true,
                    Data = _mapper.Map<IEnumerable<ShippingMethodViewModel>>(shippingMethods)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<IEnumerable<ShippingMethodViewModel>>(exception);
            }
        }

        public async Task<Response<ShippingMethodViewModel>> Get(int id)
        {
            try
            {
                var shippingMethod = await _client.GetShippingMethodAsync(id, _usingVersion);
                return new Response<ShippingMethodViewModel>
                {
                    Success = true,
                    Data = _mapper.Map<ShippingMethodViewModel>(shippingMethod)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<ShippingMethodViewModel>(exception);
            }
        }

        public async Task<Response<bool>> Exist(int id)
        {
            try
            {
                var shippingMethod = await _client.ExistShippingMethodAsync(id, _usingVersion);
                return new Response<bool>
                {
                    Success = true,
                    Data = _mapper.Map<bool>(shippingMethod)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<bool>(exception);
            }
        }

        public async Task<Response<int>> Create(CreateShippingMethodViewModel createShippingMethodViewModel)
        {
            var createShippingMethodDTO = _mapper.Map<CreateShippingMethodDTO>(createShippingMethodViewModel);

            try
            {
                var response = await _client.CreateShippingMethodAsync(_usingVersion, createShippingMethodDTO);
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

        public async Task<Response> Update(ShippingMethodViewModel shippingMethodViewModel)
        {
            var updateShippingMethodDTO = _mapper.Map<UpdateShippingMethodDTO>(shippingMethodViewModel);

            try
            {
                await _client.UpdateShippingMethodAsync(_usingVersion, updateShippingMethodDTO);
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
                await _client.DeleteShippingMethodAsync(id, _usingVersion);
                return new Response { Success = true };
            }
            catch (ApiException e)
            {
                return GenerateResponse(e);
            }
        }
    }
}
