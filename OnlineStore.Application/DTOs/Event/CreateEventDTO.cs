using AutoMapper;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.Event
{
    public class CreateEventDTO : IMapWith<Domain.Entities.Coupon>
    {
        public string? Name { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset FinishDate { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.Event, CreateEventDTO>().ReverseMap();
    }
}
