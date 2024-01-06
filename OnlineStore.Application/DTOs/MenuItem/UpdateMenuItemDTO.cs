using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Category;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.MenuItem
{
    public class UpdateMenuItemDTO : BaseDTO, IMapWith<Domain.Entities.MenuItem>
    {
        public string? Name { get; set; }

        public int CategoryId { get; set; }

        public bool IsMegaMenu { get; set; }

        public ICollection<UpdateNestedMenuItemDTO> NestedItems { get; set; } = new HashSet<UpdateNestedMenuItemDTO>();

        public string? Image { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.MenuItem, UpdateMenuItemDTO>().ReverseMap();
    }

    public class UpdateNestedMenuItemDTO : BaseDTO, IMapWith<Domain.Entities.NestedMenuItem>
    {
        public string? Name { get; set; }

        public int ParentId { get; set; }

        public ICollection<CategoryDTO> Categories { get; set; } = new HashSet<CategoryDTO>();

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.NestedMenuItem, UpdateNestedMenuItemDTO>().ReverseMap();
    }
}
