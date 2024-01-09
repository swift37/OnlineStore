using AutoMapper;
using OnlineStore.Application.Mapping;
using OnlineStore.Domain.Enums;

namespace OnlineStore.Application.DTOs.Product
{
    public class ProductsFilteringOptionsDTO : IMapWith<Domain.ProductsFilteringOptions>
    {
        public int CategoryId { get; set; }

        public int MinPrice { get; set; }

        public int MaxPrice { get; set; }

        public IDictionary<int, ICollection<int>> SpecificationIds { get; set; } = new Dictionary<int, ICollection<int>>();

        public int PageNumber { get; set; }

        public int ItemsPerPage { get; set; }

        public SortParameter SortBy { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.ProductsFilteringOptions, ProductsFilteringOptionsDTO>().ReverseMap();
    }
}
