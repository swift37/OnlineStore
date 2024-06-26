﻿using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.SpecificationType
{
    public class UpdateSpecificationTypeDTO : BaseDTO, IMapWith<Domain.Entities.SpecificationType>
    {
        public string? Name { get; set; }

        public string? DisplayName { get; set; }

        public bool IsMain { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.SpecificationType, UpdateSpecificationTypeDTO>().ReverseMap();
    }
}
