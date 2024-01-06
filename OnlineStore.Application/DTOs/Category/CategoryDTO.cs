using AutoMapper;
using OnlineStore.Application.DTOs.Base;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.Category
{
    public class CategoryDTO : BaseDTO, IMapWith<Domain.Entities.Category>
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int? RootId { get; set; }

        public int? ParentId { get; set; }

        public ICollection<CategoryDTO> ChildCategories { get; set; } = new HashSet<CategoryDTO>();

        public bool IsMainCategory { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.Category, CategoryDTO>().ReverseMap();
    }
}
