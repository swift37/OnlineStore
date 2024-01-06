using AutoMapper;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DTOs.Category
{
    public class CreateCategoryDTO : IMapWith<Domain.Entities.Category>
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int? RootId { get; set; }

        public int? ParentId { get; set; }

        public bool IsMainCategory { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Domain.Entities.Category, CreateCategoryDTO>().ReverseMap();
    }
}
