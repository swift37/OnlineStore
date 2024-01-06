﻿using AutoMapper;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.Review
{
    public class CreateReviewDTO : IMapWith<Domain.Entities.Review>
    {
        public int ProductId { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string? Title { get; set; }

        public int Rating { get; set; }

        public string? Content { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.Review, CreateReviewDTO>().ReverseMap();
    }
}
