using AutoMapper;
using OnlineStore.MVC.Models.Event;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Services
{
    public class EventsService : HttpClientServiceBase, IEventsService
    {
        public EventsService(IMapper mapper, IClient client, IHttpContextAccessor httpContextAccessor)
            : base(mapper, client, httpContextAccessor) { }

        public async Task<Response<IEnumerable<EventViewModel>>> GetAll()
        {
            try
            {
                var events = await _client.GetAllEventsAsync(_usingVersion);
                return new Response<IEnumerable<EventViewModel>>
                {
                    Success = true,
                    Data = _mapper.Map<IEnumerable<EventViewModel>>(events)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<IEnumerable<EventViewModel>>(exception);
            }
        }

        public async Task<Response<EventViewModel>> Get(int id)
        {
            try
            {
                var @event = await _client.GetEventAsync(id, _usingVersion);
                return new Response<EventViewModel>
                {
                    Success = true,
                    Data = _mapper.Map<EventViewModel>(@event)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<EventViewModel>(exception);
            }
        }

        public async Task<Response<bool>> Exist(int id)
        {
            try
            {
                var @event = await _client.ExistEventAsync(id, _usingVersion);
                return new Response<bool>
                {
                    Success = true,
                    Data = _mapper.Map<bool>(@event)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<bool>(exception);
            }
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
