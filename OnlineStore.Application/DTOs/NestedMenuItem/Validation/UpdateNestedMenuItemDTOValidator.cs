using FluentValidation;

namespace OnlineStore.Application.DTOs.NestedMenuItem.Validation
{
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
