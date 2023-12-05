using OnlineStore.MVC.Models.Wishlist;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface IWishlistsService
    {
        Task<IEnumerable<WishlistViewModel>> GetAll();

        Task<bool> Exist(int id);

        Task<WishlistViewModel> Get(int id);

        Task<Response<int>> Create(CreateWishlistViewModel createWishlistViewModel);

        Task<Response> Update(WishlistViewModel wishlistViewModel);

        Task<Response> Delete(int id);

        Task<WishlistViewModel> GetUserWishlist(Guid userId);

        Task<WishlistViewModel> GetUserWishlist();
    }
}
