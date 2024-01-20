using OnlineStore.MVC.Models.Wishlist;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface IWishlistsService
    {
        Task<Response<IEnumerable<WishlistViewModel>>> GetAll();

        Task<Response<bool>> Exist(int id);

        Task<Response<WishlistViewModel>> Get(int id);

        Task<Response<int>> Create(CreateWishlistViewModel createWishlistViewModel);

        Task<Response> Update(WishlistViewModel wishlistViewModel);

        Task<Response> Delete(int id);

        Task<Response<WishlistViewModel>> GetUserWishlist(Guid userId);

        Task<Response<WishlistViewModel>> GetUserWishlist();

        Task<Response> AddItem(CreateWishlistItemViewModel model);

        Task<Response> UpdateItem(WishlistItemViewModel model);

        Task<Response> RemoveItem(int itemId);
    }
}
