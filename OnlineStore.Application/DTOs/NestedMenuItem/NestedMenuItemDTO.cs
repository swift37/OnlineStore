using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.DTOs.Category;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.NestedMenuItem
{
    public class NestedMenuItemDTO : BaseDTO, IMapWith<Domain.Entities.NestedMenuItem>
    {
        public string? Name { get; set; }

        public int ParentId { get; set; }

        public ICollection<CategoryDTO> Categories { get; set; } = new HashSet<CategoryDTO>();

        public bool HasTwoColumns { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.NestedMenuItem, NestedMenuItemDTO>().ReverseMap();
    }
}
