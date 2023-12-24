using AutoMapper;
using OnlineStore.MVC.Models.Category;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Services
{
    public class CategoriesService : HttpClientServiceBase, ICategoriesService
    {
        public CategoriesService(IMapper mapper, IClient client, IHttpContextAccessor httpContextAccessor)
            : base(mapper, client, httpContextAccessor) { }

        public async Task<Response<IEnumerable<CategoryViewModel>>> GetAll()
        {
            try
            {
                var categories = await _client.GetAllCategoriesAsync(_usingVersion);
                return new Response<IEnumerable<CategoryViewModel>>
                {
                    Success = true,
                    Data = _mapper.Map<IEnumerable<CategoryViewModel>>(categories)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<IEnumerable<CategoryViewModel>>(exception);
            }
        }

        public async Task<Response<CategoryViewModel>> Get(int id)
        {
            try
            {
                var category = await _client.GetCategoryAsync(id, _usingVersion);
                return new Response<CategoryViewModel>
                {
                    Success = true,
                    Data = _mapper.Map<CategoryViewModel>(category)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<CategoryViewModel>(exception);
            }
        }

        public async Task<Response<bool>> Exist(int id)
        {
            try
            {
                var category = await _client.ExistCategoryAsync(id, _usingVersion);
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

        public async Task<Response<int>> Create(CreateCategoryViewModel createCategoryViewModel)
        {
            var createCategoryDTO = _mapper.Map<CreateCategoryDTO>(createCategoryViewModel);

            try
            {
                var response = await _client.CreateCategoryAsync(_usingVersion, createCategoryDTO);
                return new Response<int>
                {
                    Success = true,
                    Data = response
                };
            }
            catch (ApiException exception)
            {
                var generatedResponse = GenerateResponse(exception);
                return new Response<int>(generatedResponse);
            }
        }

        public async Task<Response> Update(CategoryViewModel categoryViewModel)
        {
            var updateCategoryDTO = _mapper.Map<UpdateCategoryDTO>(categoryViewModel);

            try
            {
                await _client.UpdateCategoryAsync(_usingVersion, updateCategoryDTO);
                return new Response { Success = true };
            }
            catch (ApiException exception)
            {
                return GenerateResponse(exception);
            }
        }

        public async Task<Response> Delete(int id)
        {
            try
            {
                await _client.DeleteCategoryAsync(id, _usingVersion);
                return new Response { Success = true };
            }
            catch (ApiException exception)
            {
                return GenerateResponse(exception);
            }
        }

        public async Task<Response<IEnumerable<CategoryViewModel>>> GetMainCategories()
        {
            try
            {
                var categories = await _client.GetMainCategoriesAsync(_usingVersion);
                return new Response<IEnumerable<CategoryViewModel>>
                {
                    Success = true,
                    Data = _mapper.Map<IEnumerable<CategoryViewModel>>(categories)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<IEnumerable<CategoryViewModel>>(exception);
            }
        }
    }
}
