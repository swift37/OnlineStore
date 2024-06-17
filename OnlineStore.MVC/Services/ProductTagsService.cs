using AutoMapper;
using OnlineStore.MVC.Models.ProductTag;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Services
{
    public class ProductTagsService : HttpClientServiceBase, IProductTagsService
    {
        public ProductTagsService(IMapper mapper, IClient client, IHttpContextAccessor httpContextAccessor)
            : base(mapper, client, httpContextAccessor) { }

        public async Task<Response<IEnumerable<ProductTagViewModel>>> GetAll()
        {
            try
            {
                var productTags = await _client.GetAllProductTagsAsync(_usingVersion);
                return new Response<IEnumerable<ProductTagViewModel>>
                {
                    Success = true,
                    Data = _mapper.Map<IEnumerable<ProductTagViewModel>>(productTags)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<IEnumerable<ProductTagViewModel>>(exception);
            }
        }

        public async Task<Response<ProductTagViewModel>> Get(int id)
        {
            try
            {
                var productTag = await _client.GetProductTagAsync(id, _usingVersion);
                return new Response<ProductTagViewModel>
                {
                    Success = true,
                    Data = _mapper.Map<ProductTagViewModel>(productTag)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<ProductTagViewModel>(exception);
            }
        }

        public async Task<Response<bool>> Exist(int id)
        {
            try
            {
                var productTag = await _client.ExistProductTagAsync(id, _usingVersion);
                return new Response<bool>
                {
                    Success = true,
                    Data = _mapper.Map<bool>(productTag)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<bool>(exception);
            }
        }

        public async Task<Response<int>> Create(CreateProductTagViewModel createProductTagViewModel)
        {
            var createProductTagDTO = _mapper.Map<CreateProductTagDTO>(createProductTagViewModel);

            try
            {
                var response = await _client.CreateProductTagAsync(_usingVersion, createProductTagDTO);
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

        public async Task<Response> Update(ProductTagViewModel productTagViewModel)
        {
            var updateProductTagDTO = _mapper.Map<UpdateProductTagDTO>(productTagViewModel);

            try
            {
                await _client.UpdateProductTagAsync(_usingVersion, updateProductTagDTO);
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
                await _client.DeleteProductTagAsync(id, _usingVersion);
                return new Response { Success = true };
            }
            catch (ApiException e)
            {
                return GenerateResponse(e);
            }
        }
    }
}
