using FluentValidation;

namespace OnlineStore.Application.DTOs.ContactRequest.Validation
{
    public class CreateContactRequestDTOValidator : AbstractValidator<CreateContactRequestDTO>
    {
        public CreateContactRequestDTOValidator()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(c => c.ContactName)
                .MaximumLength(32);

            RuleFor(c => c.Message)
                .MaximumLength(256);
        }
    }
}
