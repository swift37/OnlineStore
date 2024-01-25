using FluentValidation;

namespace OnlineStore.Application.DTOs.Subscriber.Validation
{
    public class UpdateSubscriberDTOValidator : AbstractValidator<UpdateSubscriberDTO>
    {
        public UpdateSubscriberDTOValidator()
        {
            RuleFor(s => s.Id)
                .GreaterThan(0);

            RuleFor(s => s.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
