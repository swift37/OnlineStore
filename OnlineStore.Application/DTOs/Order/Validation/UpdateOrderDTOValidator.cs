using FluentValidation;
using System.Text.RegularExpressions;

namespace OnlineStore.Application.DTOs.Order.Validation
{
    public class UpdateOrderDTOValidator : AbstractValidator<UpdateOrderDTO>
    {
        public UpdateOrderDTOValidator()
        {
            RuleFor(o => o.Id)
                .GreaterThan(0);

            RuleFor(o => o.FirstName)
                .MaximumLength(32);

            RuleFor(o => o.LastName)
                .MaximumLength(32);

            RuleFor(o => o.Email)
                .EmailAddress();

            RuleFor(o => o.Phone)
                .MaximumLength(15)
                .Matches(new Regex("^\\+?[1-9][0-9]{7,14}$"));

            RuleFor(o => o.Total)
                .GreaterThanOrEqualTo(0);

            RuleFor(o => o.TrackingNumber)
                .MaximumLength(32);

            RuleFor(o => o.Country)
                .MaximumLength(32);

            RuleFor(o => o.City)
                .MaximumLength(32);

            RuleFor(o => o.State)
                .MaximumLength(32);

            RuleFor(o => o.Postcode)
                .MaximumLength(10);

            RuleFor(o => o.StreetAddress)
                .MaximumLength(32);

            RuleFor(o => o.Apartment)
                .MaximumLength(8);

            RuleFor(o => o.Notes)
                .MaximumLength(128);
        }
    }
}
