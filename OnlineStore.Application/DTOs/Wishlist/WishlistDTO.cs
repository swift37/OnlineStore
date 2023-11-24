using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Product;

namespace OnlineStore.Application.DTOs.Wishlist
{
    public class WishlistDTO : BaseDTO
    {
        public DateTime CreateDate { get; set; }

        public DateTime LastChangeDate { get; set; }

        public ICollection<ProductDTO> Products { get; set; } = new HashSet<ProductDTO>();
    }
}
