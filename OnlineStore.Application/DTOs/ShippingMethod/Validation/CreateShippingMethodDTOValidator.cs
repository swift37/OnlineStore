using FluentValidation;

namespace OnlineStore.Application.DTOs.ShippingMethod.Validation
{
    public class CreateShippingMethodDTOValidator : AbstractValidator<CreateShippingMethodDTO>
    {
        public CreateShippingMethodDTOValidator()
        {
            RuleFor(sm => sm.Name)
                .MaximumLength(32);

            RuleFor(p => p.DisplayName)
                .MaximumLength(32);

            RuleFor(sm => sm.Price)
                .GreaterThanOrEqualTo(0);
        }
    }
}
