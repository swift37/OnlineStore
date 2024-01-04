using FluentValidation;

namespace OnlineStore.Application.DTOs.Specification.Validation
{
    public class CreateSpecificationDTOValidator : AbstractValidator<CreateSpecificationDTO>
    {
        public CreateSpecificationDTOValidator()
        {
            RuleFor(s => s.Value)
                .NotEmpty()
                .MaximumLength(32);

            RuleFor(s => s.SpecificationTypeId)
                .GreaterThan(0);
        }
    }
}
