using FluentValidation;

namespace OnlineStore.Application.DTOs.Subscriber.Validation
{
    public class UpdateSubscriberDTOValidator : AbstractValidator<UpdateSubscriberDTO>
    {
        public UpdateSubscriberDTOValidator()
        {
            RuleFor(s => s.Id)
                .NotEqual(0);

            RuleFor(s => s.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(c => c.SubscribeDate)
                .NotEqual(default(DateTime));
        }
    }
}
