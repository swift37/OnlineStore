using AutoMapper;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.ContactRequest
{
    public class CreateContactRequestDTO : IMapWith<Domain.Entities.ContactRequest>
    {
        public string? ContactName { get; set; }

        public string? Email { get; set; }

        public string? Message { get; set; }

        public bool IsConsidered { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.ContactRequest, CreateContactRequestDTO>().ReverseMap();
    }
}
