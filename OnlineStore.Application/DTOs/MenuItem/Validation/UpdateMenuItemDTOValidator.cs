using FluentValidation;

namespace OnlineStore.Application.DTOs.MenuItem.Validation
{
    public class UpdateMenuItemDTOValidator : AbstractValidator<UpdateMenuItemDTO>
    {
        public UpdateMenuItemDTOValidator()
        {
            RuleFor(i => i.Id)
                .GreaterThan(0);

            RuleFor(i => i.Name)
                .MaximumLength(32);

            RuleFor(i => i.CategoryId)
                .GreaterThan(0);
        }
    }

    public class UpdateNestedMenuItemDTOValidator : AbstractValidator<UpdateNestedMenuItemDTO>
    {
        public UpdateNestedMenuItemDTOValidator()
        {
            RuleFor(i => i.Id)
                .GreaterThan(0);

            RuleFor(i => i.Name)
                .MaximumLength(32);

            RuleFor(i => i.ParentId)
                .GreaterThan(0);
        }
    }
}
