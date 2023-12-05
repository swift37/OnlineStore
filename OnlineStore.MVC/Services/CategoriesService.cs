using AutoMapper;
using OnlineStore.MVC.Models.Category;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Services
{
    public class CategoriesService : HttpClientServiceBase, ICategoriesService
    {
        public CategoriesService(IMapper mapper, IClient client) : base(mapper, client) { }

        public Task<IEnumerable<CategoryViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CategoryViewModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exist(int id)
        {
            throw new NotImplementedException();
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
            catch (ApiException e)
            {
                var generatedResponse = GenerateResponse(e);
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
            catch (ApiException e)
            {
                return GenerateResponse(e);
            }
        }

        public async Task<Response> Delete(int id)
        {
            try
            {
                await _client.DeleteCategoryAsync(id, _usingVersion);
                return new Response { Success = true };
            }
            catch (ApiException e)
            {
                return GenerateResponse(e);
            }
        }
    }
}
