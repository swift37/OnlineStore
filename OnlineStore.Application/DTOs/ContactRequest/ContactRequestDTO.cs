﻿using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.ContactRequest
{
    public class ContactRequestDTO : BaseDTO, IMapWith<Domain.Entities.ContactRequest>
    {
        public string? ContactName { get; set; }

        public string? Email { get; set; }

        public string? Message { get; set; }

        public DateTimeOffset CreationDate { get; set; }

        public DateTimeOffset? ResponseDate { get; set; }

        public bool IsConsidered { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.ContactRequest, ContactRequestDTO>().ReverseMap();
    }
}
