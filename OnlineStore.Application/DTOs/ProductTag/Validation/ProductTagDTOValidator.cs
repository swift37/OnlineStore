using FluentValidation;

namespace OnlineStore.Application.DTOs.ProductTag.Validation
{
    public class ProductTagDTOValidator : AbstractValidator<ProductTagDTO>
    {
        public ProductTagDTOValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0);

            RuleFor(pt => pt.Name)
                .MaximumLength(32);

            RuleFor(pt => pt.DisplayName)
                .MaximumLength(32);

            RuleFor(pt => pt.ColorHex)
                .MaximumLength(7);
        }
    }
}
