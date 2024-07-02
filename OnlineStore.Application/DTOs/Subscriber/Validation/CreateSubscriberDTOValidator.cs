using FluentValidation;

namespace OnlineStore.Application.DTOs.Subscriber.Validation
{
    public class CreateSubscriberDTOValidator : AbstractValidator<CreateSubscriberDTO>
    {
        public CreateSubscriberDTOValidator()
        {
            RuleFor(s => s.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
