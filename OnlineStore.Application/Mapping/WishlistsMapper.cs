using OnlineStore.Application.DTOs;
using OnlineStore.Domain;

namespace OnlineStore.Application.Mapping
{
    public static class WishlistsMapper
    {
        public static WishlistDTO ToDTO(this Wishlist wishlist) => new WishlistDTO
        {
            Id = wishlist.Id,
            UserId = wishlist.UserId,
            CreateDate = wishlist.CreateDate,
            LastChangeDate = wishlist.LastChangeDate,
            Products = wishlist.Products.ToDTO().ToArray()
        };

        public static Wishlist FromDTO(this WishlistDTO wishlist) => new Wishlist
        {
            Id = wishlist.Id,
            UserId = wishlist.UserId,
            CreateDate = wishlist.CreateDate,
            LastChangeDate = wishlist.LastChangeDate,
            Products = wishlist.Products.FromDTO().ToArray()
        };

        public static IEnumerable<WishlistDTO> ToDTO(this IEnumerable<Wishlist> wishlists) => wishlists.Select(p => p.ToDTO());

        public static IEnumerable<Wishlist> FromDTO(this IEnumerable<WishlistDTO> wishlists) => wishlists.Select(p => p.FromDTO());
    }
}
