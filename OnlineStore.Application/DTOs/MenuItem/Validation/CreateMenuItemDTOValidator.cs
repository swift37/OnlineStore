using FluentValidation;

namespace OnlineStore.Application.DTOs.MenuItem.Validation
{
    public class CreateMenuItemDTOValidator : AbstractValidator<CreateMenuItemDTO>
    {
        public CreateMenuItemDTOValidator()
        {
            RuleFor(i => i.Name)
                .MaximumLength(32);

            RuleFor(i => i.CategoryId)
                .GreaterThan(0);
        }
    }
}
