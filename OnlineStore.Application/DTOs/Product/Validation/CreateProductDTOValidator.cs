using FluentValidation;

namespace OnlineStore.Application.DTOs.Product.Validation
{
    public class CreateProductDTOValidator : AbstractValidator<CreateProductDTO>
    {
        public CreateProductDTOValidator()
        {
            RuleFor(p => p.Name)
                .MaximumLength(32);

            RuleFor(p => p.CategoryId)
                .NotEqual(0);

            RuleFor(p => p.Discount)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.UnitPrice)
                .GreaterThan(0);

            RuleFor(p => p.UnitsInStock)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Manufacturer)
                .MaximumLength(32);

            RuleFor(p => p.ManufacturersCode)
                .MaximumLength(32);

            RuleFor(p => p.StoreCode)
                .MaximumLength(32);
        }
    }
}
