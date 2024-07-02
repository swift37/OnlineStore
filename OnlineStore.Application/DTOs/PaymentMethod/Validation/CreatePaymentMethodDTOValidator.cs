using FluentValidation;

namespace OnlineStore.Application.DTOs.PaymentMethod.Validation
{
    public class CreatePaymentMethodDTOValidator : AbstractValidator<CreatePaymentMethodDTO>
    {
        public CreatePaymentMethodDTOValidator()
        {
            RuleFor(p => p.Name)
                .MaximumLength(32);

            RuleFor(p => p.DisplayName)
                .MaximumLength(32);
        }
    }
}
