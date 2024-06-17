using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Product;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.Wishlist
{
    public class WishlistDTO : BaseDTO, IMapWith<Domain.Entities.Wishlist>
    {
        public DateTimeOffset CreationDate { get; set; }

        public DateTimeOffset LastChangeDate { get; set; }

        public ICollection<WishlistItemDTO> Items { get; set; } = new HashSet<WishlistItemDTO>();

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.Wishlist, WishlistDTO>().ReverseMap();
    }

    public class WishlistItemDTO : BaseDTO, IMapWith<Domain.Entities.WishlistItem>
    {
        public int WishlistId { get; set; }

        public int ProductId { get; set; }

        public ProductDTO? Product { get; set; }

        public int Quantity { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.WishlistItem, WishlistItemDTO>().ReverseMap();
    }
}
