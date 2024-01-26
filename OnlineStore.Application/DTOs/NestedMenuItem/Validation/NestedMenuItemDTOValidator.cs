using FluentValidation;

namespace OnlineStore.Application.DTOs.NestedMenuItem.Validation
{
    public class NestedMenuItemDTOValidator : AbstractValidator<NestedMenuItemDTO>
    {
        public NestedMenuItemDTOValidator()
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
