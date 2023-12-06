using AutoMapper;
using OnlineStore.MVC.Models.Subscriber;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Services
{
    public class SubscribersService : HttpClientServiceBase, ISubscribersService
    {
        public SubscribersService(IMapper mapper, IClient client, IHttpContextAccessor httpContextAccessor)
            : base(mapper, client, httpContextAccessor) { }

        public async Task<Response<IEnumerable<SubscriberViewModel>>> GetAll()
        {
            try
            {
                var categories = await _client.GetAllSubscribersAsync(_usingVersion);
                return new Response<IEnumerable<SubscriberViewModel>>
                {
                    Success = true,
                    Data = _mapper.Map<IEnumerable<SubscriberViewModel>>(categories)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<IEnumerable<SubscriberViewModel>>(exception);
            }
        }

        public async Task<Response<SubscriberViewModel>> Get(int id)
        {
            try
            {
                var category = await _client.GetSubscriberAsync(id, _usingVersion);
                return new Response<SubscriberViewModel>
                {
                    Success = true,
                    Data = _mapper.Map<SubscriberViewModel>(category)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<SubscriberViewModel>(exception);
            }
        }

        public async Task<Response<bool>> Exist(int id)
        {
            try
            {
                var category = await _client.ExistSubscriberAsync(id, _usingVersion);
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

        public async Task<Response<int>> Create(CreateSubscriberViewModel createSubscriberViewModel)
        {
            var createSubscriberDTO = _mapper.Map<CreateSubscriberDTO>(createSubscriberViewModel);

            try
            {
                var response = await _client.CreateSubscriberAsync(_usingVersion, createSubscriberDTO);
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

        public async Task<Response> Update(SubscriberViewModel subscriberViewModel)
        {
            var updateSubscriberDTO = _mapper.Map<UpdateSubscriberDTO>(subscriberViewModel);

            try
            {
                await _client.UpdateSubscriberAsync(_usingVersion, updateSubscriberDTO);
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
                await _client.DeleteSubscriberAsync(id, _usingVersion);
                return new Response { Success = true };
            }
            catch (ApiException e)
            {
                return GenerateResponse(e);
            }
        }
    }
}
