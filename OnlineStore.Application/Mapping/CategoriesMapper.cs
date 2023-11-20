using OnlineStore.Application.DTOs;
using OnlineStore.Domain;

namespace OnlineStore.Application.Mapping
{
    public static class CategoriesMapper
    {
        public static CategoryDTO ToDTO(this Category category) => new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            RootId = category.RootId,
            ParentId = category.ParentId,
            IsMainCategory = category.IsRootCategory
        };

        public static Category FromDTO(this CategoryDTO category) => new Category
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            RootId = category.RootId,
            ParentId = category.ParentId,
            IsRootCategory = category.IsMainCategory
        };

        public static IEnumerable<CategoryDTO> ToDTO(this IEnumerable<Category> categories) => categories.Select(c => c.ToDTO());

        public static IEnumerable<Category> FromDTO(this IEnumerable<CategoryDTO> categories) => categories.Select(c => c.FromDTO());
    }
}
