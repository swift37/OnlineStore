using FluentValidation;

namespace OnlineStore.Application.DTOs.Coupon.Validation
{
    public class CreateCouponDTOValidator : AbstractValidator<CreateCouponDTO>
    {
        public CreateCouponDTOValidator()
        {
            RuleFor(c => c.Number)
                .NotEmpty()
                .MaximumLength(16);

            RuleFor(c => c.CreationDate)
                .NotEqual(default(DateTime));

            RuleFor(c => c.StartDate)
                .NotEqual(default(DateTime));

            RuleFor(c => c.FinishDate)
                .NotEqual(default(DateTime));

            RuleFor(c => c.MaxUsesCount)
                .GreaterThan(0);

            RuleFor(c => c.MaxUsesCount)
                .GreaterThanOrEqualTo(0);

            RuleFor(c => c.DiscountSize)
                .GreaterThan(0);

            RuleFor(c => c.PercentDiscountSize)
                .GreaterThan(0.0);
        }
    }
}
