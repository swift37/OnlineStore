using FluentValidation;

namespace OnlineStore.Application.DTOs.SpecificationType.Validation
{
    public class CreateSpecificationTypeDTOValidator : AbstractValidator<CreateSpecificationTypeDTO>
    {
        public CreateSpecificationTypeDTOValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .MaximumLength(32);

            RuleFor(s => s.DisplayName)
                .NotEmpty()
                .MaximumLength(32);
        }
    }
}
