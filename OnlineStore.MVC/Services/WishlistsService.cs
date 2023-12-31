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

        public async Task<Response<IEnumerable<WishlistViewModel>>> GetAll()
        {
            try
            {
                var wishlists = await _client.GetAllWishlistsAsync(_usingVersion);
                return new Response<IEnumerable<WishlistViewModel>>
                {
                    Success = true,
                    Data = _mapper.Map<IEnumerable<WishlistViewModel>>(wishlists)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<IEnumerable<WishlistViewModel>>(exception);
            }
        }

        public async Task<Response<WishlistViewModel>> Get(int id)
        {
            try
            {
                var wishlist = await _client.GetWishlistAsync(id, _usingVersion);
                return new Response<WishlistViewModel>
                {
                    Success = true,
                    Data = _mapper.Map<WishlistViewModel>(wishlist)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<WishlistViewModel>(exception);
            }
        }

        public async Task<Response<bool>> Exist(int id)
        {
            try
            {
                var wishlist = await _client.ExistWishlistAsync(id, _usingVersion);
                return new Response<bool>
                {
                    Success = true,
                    Data = _mapper.Map<bool>(wishlist)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<bool>(exception);
            }
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

        public async Task<Response<WishlistViewModel>> GetUserWishlist(Guid userId)
        {
            try
            {
                var wishlist = await _client.GetUserWishlistAsync(userId, _usingVersion);
                return new Response<WishlistViewModel>
                {
                    Success = true,
                    Data = _mapper.Map<WishlistViewModel>(wishlist)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<WishlistViewModel>(exception);
            }
        }

        public async Task<Response<WishlistViewModel>> GetUserWishlist()
        {
            try
            {
                var wishlist = await _client.GetUserWishlistAsync(_usingVersion);
                return new Response<WishlistViewModel>
                {
                    Success = true,
                    Data = _mapper.Map<WishlistViewModel>(wishlist)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<WishlistViewModel>(exception);
            }
        }

        public async Task<Response> UpdateProducts(WishlistViewModel wishlistViewModel)
        {
            var updateWishlistDTO = _mapper.Map<UpdateWishlistDTO>(wishlistViewModel);

            try
            {
                await _client.UpdateWishlistProductsAsync(_usingVersion, updateWishlistDTO);
                return new Response { Success = true };
            }
            catch (ApiException e)
            {
                return GenerateResponse(e);
            }
        }
    }
}
