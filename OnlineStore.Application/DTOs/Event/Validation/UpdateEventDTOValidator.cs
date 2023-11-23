using FluentValidation;

namespace OnlineStore.Application.DTOs.Event.Validation
{
    public class UpdateEventDTOValidator : AbstractValidator<UpdateEventDTO>
    {
        public UpdateEventDTOValidator()
        {
            RuleFor(e => e.Id)
                .GreaterThan(0);

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
