using FluentValidation;

namespace OnlineStore.Application.DTOs.Event.Validation
{
    public class CreateEventDTOValidator : AbstractValidator<CreateEventDTO>
    {
        public CreateEventDTOValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .MaximumLength(32);

            RuleFor(e => e.StartDate)
                .NotEqual(default(DateTime));

            RuleFor(e => e.FinishDate)
                .NotEqual(default(DateTime));
        }
    }
}
