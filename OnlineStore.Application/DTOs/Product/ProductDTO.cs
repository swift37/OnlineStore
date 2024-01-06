using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Category;
using OnlineStore.Application.DTOs.Review;
using OnlineStore.Application.DTOs.Specification;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.Product
{
    public class ProductDTO : BaseDTO, IMapWith<Domain.Entities.Product>
    {
        public string? Name { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public int UnitsInStock { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public int CategoryId { get; set; }

        public CategoryDTO? Category { get; set; }

        public IEnumerable<SpecificationDTO> Specifications { get; set; } = Enumerable.Empty<SpecificationDTO>();

        public double Rating { get; set; }

        public int ReviewsCount { get; set; }

        public IEnumerable<ReviewDTO> Reviews { get; set; } = Enumerable.Empty<ReviewDTO>();

        public string? Manufacturer { get; set; }

        public string? ManufacturersCode { get; set; }

        public string? StoreCode { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsNewProduct { get; set; }

        public bool IsSale { get; set; }

        public bool IsFeaturedProduct { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.Product, ProductDTO>().ReverseMap();
    }
}
