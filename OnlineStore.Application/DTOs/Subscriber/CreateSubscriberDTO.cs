using AutoMapper;
using OnlineStore.Application.DTOs.SpecificationType;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.Subscriber
{
    public class CreateSubscriberDTO : IMapWith<Domain.Entities.Subscriber>
    {
        public string? Email { get; set; }

        public bool IsActive { get; set; } = true;

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.Subscriber, CreateSubscriberDTO>().ReverseMap();
    }
}
