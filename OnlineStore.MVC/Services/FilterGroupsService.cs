using AutoMapper;
using OnlineStore.MVC.Models.FiltersGroup;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Services
{
    public class FilterGroupsService : HttpClientServiceBase, IFilterGroupsService
    {
        public FilterGroupsService(IMapper mapper, IClient client, IHttpContextAccessor httpContextAccessor) 
            : base(mapper, client, httpContextAccessor) { }

        public async Task<Response<IEnumerable<FiltersGroupViewModel>>> GetAll()
        {
            try
            {
                var filterGroups = await _client.GetAllFilterGroupsAsync(_usingVersion);
                return new Response<IEnumerable<FiltersGroupViewModel>>
                {
                    Success = true,
                    Data = _mapper.Map<IEnumerable<FiltersGroupViewModel>>(filterGroups)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<IEnumerable<FiltersGroupViewModel>>(exception);
            }
        }

        public async Task<Response<FiltersGroupViewModel>> Get(int id)
        {
            try
            {
                var filtersGroup = await _client.GetFiltersGroupAsync(id, _usingVersion);
                return new Response<FiltersGroupViewModel>
                {
                    Success = true,
                    Data = _mapper.Map<FiltersGroupViewModel>(filtersGroup)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<FiltersGroupViewModel>(exception);
            }
        }

        public async Task<Response<bool>> Exist(int id)
        {
            try
            {
                var filtersGroup = await _client.ExistFiltersGroupAsync(id, _usingVersion);
                return new Response<bool>
                {
                    Success = true,
                    Data = _mapper.Map<bool>(filtersGroup)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<bool>(exception);
            }
        }

        public async Task<Response<int>> Create(CreateFiltersGroupViewModel createFiltersGroupViewModel)
        {
            var createFiltersGroupDTO = _mapper.Map<CreateFiltersGroupDTO>(createFiltersGroupViewModel);

            try
            {
                var response = await _client.CreateFiltersGroupAsync(_usingVersion, createFiltersGroupDTO);
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

        public async Task<Response> Update(FiltersGroupViewModel filtersGroupViewModel)
        {
            var updateFiltersGroupDTO = _mapper.Map<UpdateFiltersGroupDTO>(filtersGroupViewModel);

            try
            {
                await _client.UpdateFiltersGroupAsync(_usingVersion, updateFiltersGroupDTO);
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
                await _client.DeleteFiltersGroupAsync(id, _usingVersion);
                return new Response { Success = true };
            }
            catch (ApiException e)
            {
                return GenerateResponse(e);
            }
        }

        public async Task<Response<FiltersGroupViewModel>> GetCategoryFiltersGroup(int categoryId)
        {
            try
            {
                var filtersGroup = await _client.GetCategoryFiltersGroupAsync(categoryId, _usingVersion);
                return new Response<FiltersGroupViewModel>
                {
                    Success = true,
                    Data = _mapper.Map<FiltersGroupViewModel>(filtersGroup)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<FiltersGroupViewModel>(exception);
            }
        }
    }
}
