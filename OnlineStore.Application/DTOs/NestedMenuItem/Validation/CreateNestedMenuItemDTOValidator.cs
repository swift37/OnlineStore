using FluentValidation;

namespace OnlineStore.Application.DTOs.NestedMenuItem.Validation
{
    public class CreateNestedMenuItemDTOValidator : AbstractValidator<CreateNestedMenuItemDTO>
    {
        public CreateNestedMenuItemDTOValidator()
        {
            RuleFor(i => i.Name)
                .MaximumLength(32);

            RuleFor(i => i.ParentId)
                .GreaterThan(0);
        }
    }
}
