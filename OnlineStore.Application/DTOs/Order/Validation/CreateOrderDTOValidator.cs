using FluentValidation;
using OnlineStore.Application.Interfaces.Repositories;
using System.Text.RegularExpressions;

namespace OnlineStore.Application.DTOs.Order.Validation
{
    public class CreateOrderDTOValidator : AbstractValidator<CreateOrderDTO>
    {
        public CreateOrderDTOValidator()
        {
            RuleFor(o => o.CreatedDate)
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

    public class CreateOrderItemDTOValidator : AbstractValidator<CreateOrderItemDTO>
    {
        private readonly IProductsRepository _productsRepository;

        public CreateOrderItemDTOValidator(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;

            RuleFor(o => o.ProductId)
                .NotEqual(0);

            RuleFor(o => o.ProductId)
                .MustAsync(async (entity, value, context, cancellation) =>
                    await CheckItemAvailability(value, context, cancellation))
                .WithMessage("The {ProductName} is not available.");

            RuleFor(o => o.Quantity)
                .MustAsync(async (entity, value, context, cancellation) => 
                    await CheckItemAvailability(entity.ProductId, value, context, cancellation))
                .WithMessage("There are only {InStockQty} units of the {ProductName} in stock, so you want to buy {InOrderQty} units of the product.");

            RuleFor(o => o.Quantity)
                .GreaterThan(0)
                .WithMessage("You need to order one unit of the product at list.");
        }

        public async Task<bool> CheckItemAvailability(
            int productId,
            ValidationContext<CreateOrderItemDTO> context,
            CancellationToken cancellation = default)
        {
            var product = await _productsRepository.GetAsync(productId);
            context.MessageFormatter
                .AppendArgument("ProductName", product.Name);

            return product.IsAvailable;
        }

        public async Task<bool> CheckItemAvailability(
            int productId,
            int quantity,
            ValidationContext<CreateOrderItemDTO> context,
            CancellationToken cancellation = default)
        {
            var product = await _productsRepository.GetAsync(productId);
            context.MessageFormatter
                .AppendArgument("ProductName", product.Name)
                .AppendArgument("InOrderQty", quantity)
                .AppendArgument("InStockQty", product.UnitsInStock);

            return product.UnitsInStock >= quantity;
        }
    }
}
