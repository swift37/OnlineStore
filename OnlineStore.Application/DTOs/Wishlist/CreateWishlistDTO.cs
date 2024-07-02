using AutoMapper;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.Wishlist
{
    public class CreateWishlistDTO : IMapWith<Domain.Entities.Wishlist>
    {
        public ICollection<CreateWishlistItemDTO> Items { get; set; } = new HashSet<CreateWishlistItemDTO>();

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.Wishlist, CreateWishlistDTO>().ReverseMap();
    }

    public class CreateWishlistItemDTO : IMapWith<Domain.Entities.WishlistItem>
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.WishlistItem, CreateWishlistItemDTO>().ReverseMap();
    }
}
