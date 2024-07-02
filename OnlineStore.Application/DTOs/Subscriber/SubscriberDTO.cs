using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.Subscriber
{
    public class SubscriberDTO : BaseDTO, IMapWith<Domain.Entities.Subscriber>
    {
        public string? Email { get; set; }

        public DateTimeOffset SubscribeDate { get; set; }

        public bool IsActive { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.Subscriber, SubscriberDTO>().ReverseMap();
    }
}
