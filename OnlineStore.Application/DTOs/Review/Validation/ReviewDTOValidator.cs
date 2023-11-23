using FluentValidation;

namespace OnlineStore.Application.DTOs.Review.Validation
{
    public class ReviewDTOValidator : AbstractValidator<ReviewDTO>
    {
        public ReviewDTOValidator()
        {
            RuleFor(r => r.Id)
                .NotEqual(0);

            RuleFor(r => r.Title)
                .MaximumLength(32);

            RuleFor(r => r.CreationDate)
                .NotEqual(default(DateTime));

            RuleFor(r => r.Rating)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(5);

            RuleFor(r => r.Content)
                .MaximumLength(256);
        }
    }
}
