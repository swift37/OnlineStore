using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.Subscriber
{
    public class UpdateSubscriberDTO : BaseDTO, IMapWith<Domain.Entities.Subscriber>
    {
        public string? Email { get; set; }

        public bool IsActive { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.Subscriber, UpdateSubscriberDTO>().ReverseMap();
    }
}
