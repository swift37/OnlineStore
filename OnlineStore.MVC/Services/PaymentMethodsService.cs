using AutoMapper;
using OnlineStore.MVC.Models.PaymentMethod;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Services
{
    public class PaymentMethodsService : HttpClientServiceBase, IPaymentMethodsService
    {
        public PaymentMethodsService(IMapper mapper, IClient client, IHttpContextAccessor httpContextAccessor)
            : base(mapper, client, httpContextAccessor) { }

        public async Task<Response<IEnumerable<PaymentMethodViewModel>>> GetAll()
        {
            try
            {
                var paymentMethods = await _client.GetAllPaymentMethodsAsync(_usingVersion);
                return new Response<IEnumerable<PaymentMethodViewModel>>
                {
                    Success = true,
                    Data = _mapper.Map<IEnumerable<PaymentMethodViewModel>>(paymentMethods)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<IEnumerable<PaymentMethodViewModel>>(exception);
            }
        }

        public async Task<Response<PaymentMethodViewModel>> Get(int id)
        {
            try
            {
                var paymentMethod = await _client.GetPaymentMethodAsync(id, _usingVersion);
                return new Response<PaymentMethodViewModel>
                {
                    Success = true,
                    Data = _mapper.Map<PaymentMethodViewModel>(paymentMethod)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<PaymentMethodViewModel>(exception);
            }
        }

        public async Task<Response<bool>> Exist(int id)
        {
            try
            {
                var paymentMethod = await _client.ExistPaymentMethodAsync(id, _usingVersion);
                return new Response<bool>
                {
                    Success = true,
                    Data = _mapper.Map<bool>(paymentMethod)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<bool>(exception);
            }
        }

        public async Task<Response<int>> Create(CreatePaymentMethodViewModel createPaymentMethodViewModel)
        {
            var createPaymentMethodDTO = _mapper.Map<CreatePaymentMethodDTO>(createPaymentMethodViewModel);

            try
            {
                var response = await _client.CreatePaymentMethodAsync(_usingVersion, createPaymentMethodDTO);
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

        public async Task<Response> Update(PaymentMethodViewModel paymentMethodViewModel)
        {
            var updatePaymentMethodDTO = _mapper.Map<UpdatePaymentMethodDTO>(paymentMethodViewModel);

            try
            {
                await _client.UpdatePaymentMethodAsync(_usingVersion, updatePaymentMethodDTO);
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
                await _client.DeletePaymentMethodAsync(id, _usingVersion);
                return new Response { Success = true };
            }
            catch (ApiException e)
            {
                return GenerateResponse(e);
            }
        }
    }
}
