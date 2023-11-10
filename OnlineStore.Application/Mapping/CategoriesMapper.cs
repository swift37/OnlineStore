using OnlineStore.Application.Models;
using OnlineStore.Domain;

namespace OnlineStore.Application.Mapping
{
    public static class CategoriesMapper
    {
        public static CategoryDTO? ToDTO(this Category category) => category is null ? null : new CategoryDTO
        {
            Name = category.Name,
            Description = category.Description,
            Parent = category.Parent?.ToDTO(),
            Subcategories = category.Subcategories.ToDTO(),
            Products = category.Products.ToDTO(),
            IsMainCategory = category.IsMainCategory
        };

        public static Category? FromDTO(this CategoryDTO category) => category is null ? null : new Category
        {
            Name = category.Name,
            Description = category.Description,
            Parent = category.Parent?.FromDTO(),
            Subcategories = category.Subcategories.FromDTO(),
            Products = category.Products.FromDTO(),
            IsMainCategory = category.IsMainCategory
        };

        public static IEnumerable<CategoryDTO?> ToDTO(this IEnumerable<Category?> categories) => categories.Select(c => c?.ToDTO());

        public static IEnumerable<Category?> FromDTO(this IEnumerable<CategoryDTO?> categories) => categories.Select(c => c?.FromDTO());
    }
}
