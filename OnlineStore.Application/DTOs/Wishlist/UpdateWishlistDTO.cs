using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.Wishlist
{
    public class UpdateWishlistDTO : BaseDTO, IMapWith<Domain.Entities.Wishlist>
    {
        public DateTime LastChangeDate { get; set; } = DateTime.Now;

        public ICollection<UpdateWishlistItemDTO> Items { get; set; } = new HashSet<UpdateWishlistItemDTO>();

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.Wishlist, UpdateWishlistDTO>().ReverseMap();
    }

    public class UpdateWishlistItemDTO : BaseDTO, IMapWith<Domain.Entities.WishlistItem>
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.WishlistItem, UpdateWishlistItemDTO>().ReverseMap();
    }
}
