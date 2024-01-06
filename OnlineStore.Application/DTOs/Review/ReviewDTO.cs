﻿using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Product;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.Review
{
    public class ReviewDTO: BaseDTO, IMapWith<Domain.Entities.Review>
    {
        public int ProductId {  get; set; } 

        public ProductDTO? Product { get; set; }

        public DateTime CreationDate { get; set; }

        public string? Title { get; set; }

        public int Rating { get; set; }

        public string? Content { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.Review, ReviewDTO>().ReverseMap();
    }
}
