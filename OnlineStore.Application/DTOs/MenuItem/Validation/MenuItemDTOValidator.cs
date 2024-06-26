﻿using FluentValidation;

namespace OnlineStore.Application.DTOs.MenuItem.Validation
{
    public class MenuItemDTOValidator : AbstractValidator<MenuItemDTO>
    {
        public MenuItemDTOValidator()
        {
            RuleFor(i => i.Id)
                .GreaterThan(0);

            RuleFor(i => i.Name)
                .MaximumLength(32);

            RuleFor(i => i.CategoryId)
                .GreaterThan(0);
        }
    }
}
