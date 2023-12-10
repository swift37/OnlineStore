using OnlineStore.Application.DTOs.MenuItem;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Mapping
{
    public static class MenuItemsMapper
    {
        public static MenuItemDTO ToDTO(this MenuItem menuItem) => new MenuItemDTO
        {
            Id = menuItem.Id,
            Name = menuItem.Name,
            CategoryId = menuItem.CategoryId,
            IsMegaMenu = menuItem.IsMegaMenu,
            NestedItems = menuItem.NestedItems.ToDTO().ToArray(),
            Image = menuItem.Image
        };

        public static MenuItem FromDTO(this MenuItemDTO menuItem) => new MenuItem
        {
            Id = menuItem.Id,
            Name = menuItem.Name,
            CategoryId = menuItem.CategoryId,
            IsMegaMenu = menuItem.IsMegaMenu,
            NestedItems = menuItem.NestedItems.FromDTO().ToArray(),
            Image = menuItem.Image
        };

        public static MenuItem FromDTO(this CreateMenuItemDTO menuItem) => new MenuItem
        {
            Name = menuItem.Name,
            CategoryId = menuItem.CategoryId,
            IsMegaMenu = menuItem.IsMegaMenu,
            NestedItems = menuItem.NestedItems.FromDTO().ToArray(),
            Image = menuItem.Image
        };

        public static MenuItem FromDTO(this UpdateMenuItemDTO menuItem) => new MenuItem
        {
            Id = menuItem.Id,
            Name = menuItem.Name,
            CategoryId = menuItem.CategoryId,
            IsMegaMenu = menuItem.IsMegaMenu,
            NestedItems = menuItem.NestedItems.FromDTO().ToArray(),
            Image = menuItem.Image
        };

        public static NestedMenuItemDTO ToDTO(this NestedMenuItem nestedMenuItem) => new NestedMenuItemDTO
        {
            Id = nestedMenuItem.Id,
            Name = nestedMenuItem.Name,
            ParentId = nestedMenuItem.ParentId,
            Categories = nestedMenuItem.Categories.ToDTO().ToArray(),
        };

        public static NestedMenuItem FromDTO(this NestedMenuItemDTO nestedMenuItem) => new NestedMenuItem
        {
            Id = nestedMenuItem.Id,
            Name = nestedMenuItem.Name,
            ParentId = nestedMenuItem.ParentId,
            Categories = nestedMenuItem.Categories.FromDTO().ToArray()
        };

        public static NestedMenuItem FromDTO(this CreateNestedMenuItemDTO nestedMenuItem) => new NestedMenuItem
        {
            Name = nestedMenuItem.Name,
            ParentId = nestedMenuItem.ParentId,
            Categories = nestedMenuItem.Categories.FromDTO().ToArray()
        };

        public static NestedMenuItem FromDTO(this UpdateNestedMenuItemDTO nestedMenuItem) => new NestedMenuItem
        {
            Id = nestedMenuItem.Id,
            Name = nestedMenuItem.Name,
            ParentId = nestedMenuItem.ParentId,
            Categories = nestedMenuItem.Categories.FromDTO().ToArray()
        };

        public static IEnumerable<MenuItemDTO> ToDTO(this IEnumerable<MenuItem> menuItems) => menuItems.Select(p => p.ToDTO());

        public static IEnumerable<MenuItem> FromDTO(this IEnumerable<MenuItemDTO> menuItems) => menuItems.Select(p => p.FromDTO());

        public static IEnumerable<NestedMenuItemDTO> ToDTO(this IEnumerable<NestedMenuItem> nestedMenuItems) => nestedMenuItems.Select(p => p.ToDTO());

        public static IEnumerable<NestedMenuItem> FromDTO(this IEnumerable<NestedMenuItemDTO> nestedMenuItems) => nestedMenuItems.Select(p => p.FromDTO());

        public static IEnumerable<NestedMenuItem> FromDTO(this IEnumerable<CreateNestedMenuItemDTO> nestedMenuItems) => nestedMenuItems.Select(p => p.FromDTO());

        public static IEnumerable<NestedMenuItem> FromDTO(this IEnumerable<UpdateNestedMenuItemDTO> nestedMenuItems) => nestedMenuItems.Select(p => p.FromDTO());
    }
}
