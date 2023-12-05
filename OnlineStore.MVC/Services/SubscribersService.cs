using AutoMapper;
using OnlineStore.MVC.Models.Subscriber;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Services
{
    public class SubscribersService : HttpClientServiceBase, ISubscribersService
    {
        public SubscribersService(IMapper mapper, IClient client) : base(mapper, client) { }

        public Task<IEnumerable<SubscriberViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<SubscriberViewModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exist(int id)
        {
            throw new NotImplementedException();
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
