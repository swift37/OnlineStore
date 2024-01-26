using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.NestedMenuItem
{
    public class UpdateNestedMenuItemDTO : BaseDTO, IMapWith<Domain.Entities.NestedMenuItem>
    {
        public string? Name { get; set; }

        public int ParentId { get; set; }

        public ICollection<int> CategoryIds { get; set; } = new HashSet<int>();

        public bool HasTwoColumns { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.NestedMenuItem, UpdateNestedMenuItemDTO>().ReverseMap();
    }
}
