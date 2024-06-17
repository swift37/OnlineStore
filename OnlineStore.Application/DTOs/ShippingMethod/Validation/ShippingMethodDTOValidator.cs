using FluentValidation;

namespace OnlineStore.Application.DTOs.ShippingMethod.Validation
{
    public class ShippingMethodDTOValidator : AbstractValidator<ShippingMethodDTO>
    {
        public ShippingMethodDTOValidator()
        {
            RuleFor(sm => sm.Id)
                .GreaterThan(0);

            RuleFor(sm => sm.Name)
                .MaximumLength(32);

            RuleFor(p => p.DisplayName)
                .MaximumLength(32);

            RuleFor(sm => sm.Price)
                .GreaterThanOrEqualTo(0);
        }
    }
}
