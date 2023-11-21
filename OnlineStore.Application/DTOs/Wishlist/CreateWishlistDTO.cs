using OnlineStore.Application.DTOs.Product;

namespace OnlineStore.Application.DTOs.Wishlist
{
    public class CreateWishlistDTO
    {
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public DateTime LastChangeDate { get; set; } = DateTime.Now;

        public ICollection<ProductDTO> Products { get; set; } = new HashSet<ProductDTO>();
    }
}
