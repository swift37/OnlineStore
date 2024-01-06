using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Product;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.Wishlist
{
    public class UpdateWishlistDTO : BaseDTO, IMapWith<Domain.Entities.Wishlist>
    {
        public DateTime LastChangeDate { get; set; } = DateTime.Now;

        public ICollection<ProductDTO> Products { get; set; } = new HashSet<ProductDTO>();

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.Wishlist, UpdateWishlistDTO>().ReverseMap();
    }
}
