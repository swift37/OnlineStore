using AutoMapper;
using OnlineStore.Application.DTOs.Category;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.Product
{
    public class ProductsPageDTO : IMapWith<Domain.ProductsPage>
    {
        public ICollection<ProductDTO> Products { get; set; } = new HashSet<ProductDTO>();

        public CategoryDTO? Category { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int ItemsPerPage { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.ProductsPage, ProductsPageDTO>().ReverseMap();
    }
}
