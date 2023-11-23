using FluentValidation;

namespace OnlineStore.Application.DTOs.ContactRequest.Validation
{
    public class UpdateContactRequestDTOValidator : AbstractValidator<UpdateContactRequestDTO>
    {
        public UpdateContactRequestDTOValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0);

            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(c => c.Message)
                .MaximumLength(256);

            RuleFor(c => c.ResponseDate)
                .NotEqual(default(DateTime));
        }
    }
}
