using AutoMapper;
using OnlineStore.MVC.Models.Wishlist;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Services
{
    public class WishlistsService : HttpClientServiceBase, IWishlistsService
    {
        public WishlistsService(IMapper mapper, IClient client, IHttpContextAccessor httpContextAccessor)
            : base(mapper, client, httpContextAccessor) { }

        public Task<Response<IEnumerable<WishlistViewModel>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Response<WishlistViewModel>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> Exist(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<int>> Create(CreateWishlistViewModel createWishlistViewModel)
        {
            var createWishlistDTO = _mapper.Map<CreateWishlistDTO>(createWishlistViewModel);

            try
            {
                var response = await _client.CreateWishlistAsync(_usingVersion, createWishlistDTO);
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

        public async Task<Response> Update(WishlistViewModel wishlistViewModel)
        {
            var updateWishlistDTO = _mapper.Map<UpdateWishlistDTO>(wishlistViewModel);

            try
            {
                await _client.UpdateWishlistAsync(_usingVersion, updateWishlistDTO);
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
                await _client.DeleteWishlistAsync(id, _usingVersion);
                return new Response { Success = true };
            }
            catch (ApiException e)
            {
                return GenerateResponse(e);
            }
        }

        public Task<Response<WishlistViewModel>> GetUserWishlist(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<WishlistViewModel>> GetUserWishlist()
        {
            throw new NotImplementedException();
        }
    }
}
