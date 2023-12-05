using AutoMapper;
using OnlineStore.MVC.Models.Event;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Services
{
    public class EventsService : HttpClientServiceBase, IEventsService
    {
        public EventsService(IMapper mapper, IClient client) : base(mapper, client) { }

        public Task<IEnumerable<EventViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<EventViewModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exist(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<int>> Create(CreateEventViewModel createEventViewModel)
        {
            var createEventDTO = _mapper.Map<CreateEventDTO>(createEventViewModel);

            try
            {
                var response = await _client.CreateEventAsync(_usingVersion, createEventDTO);
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

        public async Task<Response> Update(EventViewModel eventViewModel)
        {
            var updateEventDTO = _mapper.Map<UpdateEventDTO>(eventViewModel);

            try
            {
                await _client.UpdateEventAsync(_usingVersion, updateEventDTO);
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
                await _client.DeleteEventAsync(id, _usingVersion);
                return new Response { Success = true };
            }
            catch (ApiException e)
            {
                return GenerateResponse(e);
            }
        }
    }
}
