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

        public Task<Response<IEnumerable<ProductViewModel>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Response<ProductViewModel>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> Exist(int id)
        {
            throw new NotImplementedException();
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

        public Task<Response<ProductsPageViewModel>> GetProductsByCategory(
            int categoryId, 
            int page = 1, 
            int itemsPerPage = 15, 
            Models.Enums.SortParameters sortBy = Models.Enums.SortParameters.Default)
        {
            throw new NotImplementedException();
        }
    }
}
