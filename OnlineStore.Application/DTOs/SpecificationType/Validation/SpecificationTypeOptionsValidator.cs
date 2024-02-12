using FluentValidation;
using OnlineStore.Domain;

namespace OnlineStore.Application.DTOs.SpecificationType.Validation
{
    public class SpecificationTypeOptionsValidator : AbstractValidator<SpecificationTypeOptions>
    {
        public SpecificationTypeOptionsValidator()
        {
            RuleFor(opt => opt.Id)
                .GreaterThan(0);
        }
    }
}
