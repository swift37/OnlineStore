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

            RuleFor(o => o.Number)
                .Length(16)
                .Matches(new Regex("^[0-9]{16}$"));

            RuleFor(o => o.PayDate)
                .NotEqual(default(DateTime));

            RuleFor(o => o.ShippedDate)
                .NotEqual(default(DateTime));

            RuleFor(o => o.FirstName)
                .MaximumLength(32);

            RuleFor(o => o.LastName)
                .MaximumLength(32);

            RuleFor(o => o.Email)
                .EmailAddress();

            RuleFor(o => o.Phone)
                .MaximumLength(15)
                .Matches(new Regex("^\\+?[1-9][0-9]{7,14}$"));

            RuleFor(o => o.ShippingCost)
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

    public class UpdateOrderItemDTOValidator : AbstractValidator<UpdateOrderItemDTO>
    {
        public UpdateOrderItemDTOValidator()
        {
            RuleFor(o => o.Id)
                .GreaterThan(0);

            RuleFor(o => o.UnitPrice)
                .GreaterThan(0);

            RuleFor(o => o.Discount)
                .GreaterThanOrEqualTo(0);

            RuleFor(o => o.Quantity)
                .GreaterThan(0);
        }
    }
}
