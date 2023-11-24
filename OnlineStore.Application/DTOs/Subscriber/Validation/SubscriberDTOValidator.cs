using FluentValidation;

namespace OnlineStore.Application.DTOs.Subscriber.Validation
{
    public class SubscriberDTOValidator : AbstractValidator<SubscriberDTO>
    {
        public SubscriberDTOValidator()
        {
            RuleFor(s => s.Id)
                .GreaterThan(0);

            RuleFor(s => s.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(s => s.SubscribeDate)
                .NotEqual(default(DateTime));
        }
    }
}
