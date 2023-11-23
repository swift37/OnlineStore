using FluentValidation;

namespace OnlineStore.Application.DTOs.ContactRequest.Validation
{
    public class ContactRequestDTOValidator : AbstractValidator<ContactRequestDTO>
    {
        public ContactRequestDTOValidator()
        {
            RuleFor(c => c.Id)
                .NotEqual(0);

            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(c => c.Message)
                .MaximumLength(256);

            RuleFor(c => c.CreationDate)
                .NotEqual(default(DateTime));

            RuleFor(c => c.ResponseDate)
                .NotEqual(default(DateTime));
        }
    }
}
