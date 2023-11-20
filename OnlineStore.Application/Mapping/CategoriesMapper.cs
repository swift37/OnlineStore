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
            Root = category.Root?.ToDTO(),
            Parent = category.Parent?.ToDTO(),
            Subcategories = category.Subcategories.ToDTO(),
            Products = category.Products.ToDTO(),
            IsMainCategory = category.IsRootCategory
        };

        public static Category FromDTO(this CategoryDTO category) => new Category
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Root = category.Root?.FromDTO(),
            Parent = category.Parent?.FromDTO(),
            Subcategories = category.Subcategories.FromDTO(),
            Products = category.Products.FromDTO(),
            IsRootCategory = category.IsMainCategory
        };

        public static IEnumerable<CategoryDTO> ToDTO(this IEnumerable<Category> categories) => categories.Select(c => c.ToDTO());

        public static IEnumerable<Category> FromDTO(this IEnumerable<CategoryDTO> categories) => categories.Select(c => c.FromDTO());
    }
}
