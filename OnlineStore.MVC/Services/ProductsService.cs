using AutoMapper;
using OnlineStore.MVC.Models.Product;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Services
{
    public class ProductsService : HttpClientServiceBase, IProductsService
    {
        public ProductsService(IMapper mapper, IClient client, IHttpContextAccessor httpContextAccessor)
            : base(mapper, client, httpContextAccessor) { }

        public async Task<Response<IEnumerable<ProductViewModel>>> GetAll()
        {
            try
            {
                var categories = await _client.GetAllProductsAsync(_usingVersion);
                return new Response<IEnumerable<ProductViewModel>>
                {
                    Success = true,
                    Data = _mapper.Map<IEnumerable<ProductViewModel>>(categories)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<IEnumerable<ProductViewModel>>(exception);
            }
        }

        public async Task<Response<ProductViewModel>> Get(int id)
        {
            try
            {
                var category = await _client.GetProductAsync(id, _usingVersion);
                return new Response<ProductViewModel>
                {
                    Success = true,
                    Data = _mapper.Map<ProductViewModel>(category)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<ProductViewModel>(exception);
            }
        }

        public async Task<Response<bool>> Exist(int id)
        {
            try
            {
                var category = await _client.ExistProductAsync(id, _usingVersion);
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

        public async Task<Response<int>> Create(CreateProductViewModel createProductViewModel)
        {
            var createProductDTO = _mapper.Map<CreateProductDTO>(createProductViewModel);

            try
            {
                var response = await _client.CreateProductAsync(_usingVersion, createProductDTO);
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

        public async Task<Response> Update(ProductViewModel productViewModel)
        {
            var updateProductDTO = _mapper.Map<UpdateProductDTO>(productViewModel);

            try
            {
                await _client.UpdateProductAsync(_usingVersion, updateProductDTO);
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
                await _client.DeleteProductAsync(id, _usingVersion);
                return new Response { Success = true };
            }
            catch (ApiException e)
            {
                return GenerateResponse(e);
            }
        }

        public async Task<Response<ProductsPageViewModel>> GetProductsByCategory(
            int categoryId, 
            int page = 1, 
            int itemsPerPage = 15, 
            Models.Enums.SortParameters sortBy = Models.Enums.SortParameters.Default)
        {
            try
            {
                //var category = await _client
                //    .GetProductsByCategoryAsync(categoryId, page, itemsPerPage, sortBy, _usingVersion);
                return new Response<ProductsPageViewModel>
                {
                    Success = true,
                    //Data = _mapper.Map<ProductsPageViewModel>(category)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<ProductsPageViewModel>(exception);
            }
        }
    }
}
