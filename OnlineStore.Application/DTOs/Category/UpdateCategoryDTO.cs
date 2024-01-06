﻿using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.Category
{
    public class UpdateCategoryDTO : BaseDTO, IMapWith<Domain.Entities.Category>
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int? RootId { get; set; }

        public int? ParentId { get; set; }

        public bool IsMainCategory { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.Category, UpdateCategoryDTO>().ReverseMap();
    }
}
