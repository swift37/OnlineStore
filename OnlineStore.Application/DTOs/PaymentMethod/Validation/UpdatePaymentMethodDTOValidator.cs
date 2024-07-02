using FluentValidation;

namespace OnlineStore.Application.DTOs.PaymentMethod.Validation
{
    public class UpdatePaymentMethodDTOValidator : AbstractValidator<UpdatePaymentMethodDTO>
    {
        public UpdatePaymentMethodDTOValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0);

            RuleFor(p => p.Name)
                .MaximumLength(32);

            RuleFor(p => p.DisplayName)
                .MaximumLength(32);
        }
    }
}
