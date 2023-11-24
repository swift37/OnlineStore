using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Product;

namespace OnlineStore.Application.DTOs.Wishlist
{
    public class UpdateWishlistDTO : BaseDTO
    {
        public DateTime LastChangeDate { get; set; } = DateTime.Now;

        public ICollection<ProductDTO> Products { get; set; } = new HashSet<ProductDTO>();
    }
}
