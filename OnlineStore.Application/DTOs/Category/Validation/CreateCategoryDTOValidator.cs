using FluentValidation;

namespace OnlineStore.Application.DTOs.Category.Validation
{
    public class CreateCategoryDTOValidator : AbstractValidator<CreateCategoryDTO>
    {
        public CreateCategoryDTOValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(32);

            RuleFor(c => c.Description)
                .MaximumLength(128);
        }
    }
}
