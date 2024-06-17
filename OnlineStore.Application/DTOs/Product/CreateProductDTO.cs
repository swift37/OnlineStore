﻿using AutoMapper;
using OnlineStore.Application.DTOs.ProductTag;
using OnlineStore.Application.DTOs.Specification;
using OnlineStore.Application.Mapping;
using OnlineStore.Domain.Enums;

namespace OnlineStore.Application.DTOs.Product
{
    public class CreateProductDTO : IMapWith<Domain.Entities.Product>
    {
        public string? Name { get; set; }

        public decimal UnitCost { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public int UnitsInStock { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<SpecificationDTO> Specifications { get; set; } = Enumerable.Empty<SpecificationDTO>();

        public IEnumerable<ProductTagDTO> Tags { get; set; } = new HashSet<ProductTagDTO>();

        public string? Manufacturer { get; set; }

        public string? ManufacturersCode { get; set; }

        public string? StoreCode { get; set; }

        public ProductStatus Status { get; set; }

        public ProductAvailability Availability { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.Product, CreateProductDTO>().ReverseMap();
    }
}
