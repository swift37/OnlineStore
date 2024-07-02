using FluentValidation;

namespace OnlineStore.Application.DTOs.Product.Validation
{
    public class ProductsFilteringOptionsDTOValidator : AbstractValidator<ProductsFilteringOptionsDTO>
    {
        public ProductsFilteringOptionsDTOValidator()
        {
            RuleFor(opt => opt.CategoryId)
                .GreaterThan(0);

            RuleFor(opt => opt.MinPrice)
                .GreaterThanOrEqualTo(0);

            RuleFor(opt => opt.MaxPrice)
                .GreaterThan(0);

            RuleFor(opt => opt.PageNumber)
                .GreaterThan(0);

            RuleFor(opt => opt.ItemsPerPage)
                .GreaterThanOrEqualTo(15);
        }
    }
}
