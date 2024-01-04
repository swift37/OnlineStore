using FluentValidation;

namespace OnlineStore.Application.DTOs.Specification.Validation
{
    public class UpdateSpecificationDTOValidator : AbstractValidator<UpdateSpecificationDTO>
    {
        public UpdateSpecificationDTOValidator()
        {
            RuleFor(s => s.Id)
                .GreaterThan(0);

            RuleFor(s => s.Value)
                .NotEmpty()
                .MaximumLength(32);

            RuleFor(s => s.SpecificationTypeId)
                .GreaterThan(0);
        }
    }
}
