using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Product;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.Wishlist
{
    public class WishlistDTO : BaseDTO, IMapWith<Domain.Entities.Wishlist>
    {
        public DateTime CreateDate { get; set; }

        public DateTime LastChangeDate { get; set; }

        public ICollection<ProductDTO> Products { get; set; } = new HashSet<ProductDTO>();

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.Wishlist, WishlistDTO>().ReverseMap();
    }
}
