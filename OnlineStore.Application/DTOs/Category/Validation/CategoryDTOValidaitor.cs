using FluentValidation;

namespace OnlineStore.Application.DTOs.Category.Validation
{
    public class CategoryDTOValidaitor : AbstractValidator<CategoryDTO>
    {
        public CategoryDTOValidaitor()
        {
            RuleFor(c => c.Id)
                .NotEqual(0);

            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(32);

            RuleFor(c => c.Description)
                .MaximumLength(128);
        }
    }
}
