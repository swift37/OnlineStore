using AutoMapper;
using OnlineStore.MVC.Models.MenuItem;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Services
{
    public class MenuItemsService : HttpClientServiceBase, IMenuItemsService
    {
        public MenuItemsService(IMapper mapper, IClient client, IHttpContextAccessor httpContextAccessor) 
            : base(mapper, client, httpContextAccessor) { }

        public async Task<Response<IEnumerable<MenuItemViewModel>>> GetAll()
        {
            try
            {
                var menuItems = await _client.GetAllMenuItemsAsync(_usingVersion);
                return new Response<IEnumerable<MenuItemViewModel>>
                {
                    Success = true,
                    Data = _mapper.Map<IEnumerable<MenuItemViewModel>>(menuItems)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<IEnumerable<MenuItemViewModel>>(exception);
            }
        }

        public async Task<Response<MenuItemViewModel>> Get(int id)
        {
            try
            {
                var menuItem = await _client.GetMenuItemAsync(id, _usingVersion);
                return new Response<MenuItemViewModel>
                {
                    Success = true,
                    Data = _mapper.Map<MenuItemViewModel>(menuItem)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<MenuItemViewModel>(exception);
            }
        }

        public async Task<Response<bool>> Exist(int id)
        {
            try
            {
                var menuItem = await _client.ExistMenuItemAsync(id, _usingVersion);
                return new Response<bool>
                {
                    Success = true,
                    Data = _mapper.Map<bool>(menuItem)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<bool>(exception);
            }
        }

        public async Task<Response<int>> Create(CreateMenuItemViewModel createMenuItemViewModel)
        {
            var createMenuItemDTO = _mapper.Map<CreateMenuItemDTO>(createMenuItemViewModel);

            try
            {
                var response = await _client.CreateMenuItemAsync(_usingVersion, createMenuItemDTO);
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

        public async Task<Response> Update(MenuItemViewModel menuItemViewModel)
        {
            var updateMenuItemDTO = _mapper.Map<UpdateMenuItemDTO>(menuItemViewModel);

            try
            {
                await _client.UpdateMenuItemAsync(_usingVersion, updateMenuItemDTO);
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
                await _client.DeleteMenuItemAsync(id, _usingVersion);
                return new Response { Success = true };
            }
            catch (ApiException e)
            {
                return GenerateResponse(e);
            }
        }
    }
}
