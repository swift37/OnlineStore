using AutoMapper;
using OnlineStore.Application.DTOs.Product;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.Wishlist
{
    public class CreateWishlistDTO : IMapWith<Domain.Entities.Wishlist>
    {
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public DateTime LastChangeDate { get; set; } = DateTime.Now;

        public ICollection<ProductDTO> Products { get; set; } = new HashSet<ProductDTO>();

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.Wishlist, CreateWishlistDTO>().ReverseMap();
    }
}
