using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.Review
{
    public class UpdateReviewDTO : BaseDTO, IMapWith<Domain.Entities.Review>
    {
        public string? Name { get; set; }

        public int Rating { get; set; }

        public string? Content { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.Review, UpdateReviewDTO>().ReverseMap();
    }
}
