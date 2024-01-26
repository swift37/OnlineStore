using FluentValidation;

namespace OnlineStore.Application.DTOs.SpecificationType.Validation
{
    public class UpdateSpecificationTypeDTOValidator : AbstractValidator<UpdateSpecificationTypeDTO>
    {
        public UpdateSpecificationTypeDTOValidator()
        {
            RuleFor(s => s.Id)
                .GreaterThan(0);

            RuleFor(s => s.Name)
                .NotEmpty()
                .MaximumLength(32);

            RuleFor(s => s.DisplayName)
                .NotEmpty()
                .MaximumLength(32);
        }
    }
}
