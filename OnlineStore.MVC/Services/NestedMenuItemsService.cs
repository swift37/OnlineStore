using AutoMapper;
using OnlineStore.MVC.Models.NestedMenuItem;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Services
{
    public class NestedMenuItemsService : HttpClientServiceBase, INestedMenuItemsService
    {
        public NestedMenuItemsService(IMapper mapper, IClient client, IHttpContextAccessor httpContextAccessor)
            : base(mapper, client, httpContextAccessor) { }

        public async Task<Response<IEnumerable<NestedMenuItemViewModel>>> GetAll()
        {
            try
            {
                var nestedMenuItems = await _client.GetAllNestedMenuItemsAsync(_usingVersion);
                return new Response<IEnumerable<NestedMenuItemViewModel>>
                {
                    Success = true,
                    Data = _mapper.Map<IEnumerable<NestedMenuItemViewModel>>(nestedMenuItems)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<IEnumerable<NestedMenuItemViewModel>>(exception);
            }
        }

        public async Task<Response<NestedMenuItemViewModel>> Get(int id)
        {
            try
            {
                var nestedMenuItem = await _client.GetNestedMenuItemAsync(id, _usingVersion);
                return new Response<NestedMenuItemViewModel>
                {
                    Success = true,
                    Data = _mapper.Map<NestedMenuItemViewModel>(nestedMenuItem)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<NestedMenuItemViewModel>(exception);
            }
        }

        public async Task<Response<bool>> Exist(int id)
        {
            try
            {
                var nestedMenuItem = await _client.ExistNestedMenuItemAsync(id, _usingVersion);
                return new Response<bool>
                {
                    Success = true,
                    Data = _mapper.Map<bool>(nestedMenuItem)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<bool>(exception);
            }
        }

        public async Task<Response<int>> Create(CreateNestedMenuItemViewModel createNestedMenuItemViewModel)
        {
            var createNestedMenuItemDTO = _mapper.Map<CreateNestedMenuItemDTO>(createNestedMenuItemViewModel);

            try
            {
                var response = await _client.CreateNestedMenuItemAsync(_usingVersion, createNestedMenuItemDTO);
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

        public async Task<Response> Update(UpdateNestedMenuItemViewModel updateNestedMenuItemViewModel)
        {
            var updateNestedMenuItemDTO = _mapper.Map<UpdateNestedMenuItemDTO>(updateNestedMenuItemViewModel);

            try
            {
                await _client.UpdateNestedMenuItemAsync(_usingVersion, updateNestedMenuItemDTO);
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
                await _client.DeleteNestedMenuItemAsync(id, _usingVersion);
                return new Response { Success = true };
            }
            catch (ApiException e)
            {
                return GenerateResponse(e);
            }
        }
    }
}
