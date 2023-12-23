using FluentValidation;
using System.Text.RegularExpressions;

namespace OnlineStore.Application.DTOs.Order.Validation
{
    public class OrderDTOValidator : AbstractValidator<OrderDTO>
    {
        public OrderDTOValidator()
        {
            RuleFor(o => o.Id)
                .GreaterThan(0);

            RuleFor(o => o.Number)
                .Length(16)
                .Matches(new Regex("^[0-9]{16}$"));

            RuleFor(o => o.CreatedDate)
                .NotEqual(default(DateTime));

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
                .MaximumLength(16)
                .Matches(new Regex("^\\+?[1-9][0-9]{7,14}$"));

            RuleFor(o => o.Total)
                .GreaterThanOrEqualTo(0);

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
                .MaximumLength(64);
        }
    }

    public class OrderItemDTOValidator : AbstractValidator<OrderItemDTO>
    {
        public OrderItemDTOValidator()
        {
            RuleFor(o => o.Id)
                .GreaterThan(0);

            RuleFor(o => o.OrderId)
                .GreaterThan(0);

            RuleFor(o => o.ProductId)
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
