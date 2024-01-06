using AutoMapper;
using OnlineStore.Application.DTOs.Category;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.MenuItem
{
    public class CreateMenuItemDTO : IMapWith<Domain.Entities.MenuItem>
    {
        public string? Name { get; set; }

        public int CategoryId { get; set; }

        public bool IsMegaMenu { get; set; }

        public ICollection<CreateNestedMenuItemDTO> NestedItems { get; set; } = new HashSet<CreateNestedMenuItemDTO>();

        public string? Image {  get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.MenuItem, CreateMenuItemDTO>().ReverseMap();
    }

    public class CreateNestedMenuItemDTO : IMapWith<Domain.Entities.NestedMenuItem>
    {
        public string? Name { get; set; }

        public int ParentId { get; set; }

        public ICollection<CategoryDTO> Categories { get; set; } = new HashSet<CategoryDTO>();

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.NestedMenuItem, CreateNestedMenuItemDTO>().ReverseMap();
    }
}
