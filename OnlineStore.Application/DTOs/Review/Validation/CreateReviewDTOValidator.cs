using FluentValidation;

namespace OnlineStore.Application.DTOs.Review.Validation
{
    public class CreateReviewDTOValidator : AbstractValidator<CreateReviewDTO>
    {
        public CreateReviewDTOValidator()
        {
            RuleFor(r => r.Name)
                .MaximumLength(32);

            RuleFor(r => r.ProductId)
                .NotEqual(0);

            RuleFor(r => r.Rating)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(5);

            RuleFor(r => r.Content)
                .MaximumLength(256);
        }
    }
}
