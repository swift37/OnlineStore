using FluentValidation;

namespace OnlineStore.Application.DTOs.Product.Validation
{
    public class ProductDTOValidator : AbstractValidator<ProductDTO>
    {
        public ProductDTOValidator()
        {
            RuleFor(p => p.Id)
                .NotEqual(0);

            RuleFor(p => p.Name)
                .MaximumLength(32);

            RuleFor(p => p.Rating)
                .GreaterThan(0)
                .LessThanOrEqualTo(5);

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

    public class SpecificationDTOValidator : AbstractValidator<SpecificationDTO>
    {
        public SpecificationDTOValidator()
        {
            RuleFor(s => s.Id)
                .NotEqual(0);

            RuleFor(s => s.Name)
                .MaximumLength(32);

            RuleFor(s => s.Value)
                .MaximumLength(32);
        }
    }
}
