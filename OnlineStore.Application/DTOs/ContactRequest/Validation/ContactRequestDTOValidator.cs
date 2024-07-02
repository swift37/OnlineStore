using FluentValidation;

namespace OnlineStore.Application.DTOs.ContactRequest.Validation
{
    public class ContactRequestDTOValidator : AbstractValidator<ContactRequestDTO>
    {
        public ContactRequestDTOValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0);

            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(c => c.ContactName)
                .MaximumLength(32);

            RuleFor(c => c.Message)
                .MaximumLength(256);

            RuleFor(c => c.CreationDate)
                .NotEqual(default(DateTime));

            RuleFor(c => c.ResponseDate)
                .NotEqual(default(DateTime));
        }
    }
}
