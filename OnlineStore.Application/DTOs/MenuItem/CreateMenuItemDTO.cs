using AutoMapper;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.MenuItem
{
    public class CreateMenuItemDTO : IMapWith<Domain.Entities.MenuItem>
    {
        public string? Name { get; set; }

        public int CategoryId { get; set; }

        public bool IsMegaMenu { get; set; }

        public string? Image {  get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.MenuItem, CreateMenuItemDTO>().ReverseMap();
    }
}
