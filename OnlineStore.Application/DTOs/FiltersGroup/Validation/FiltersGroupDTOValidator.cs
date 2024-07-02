using FluentValidation;

namespace OnlineStore.Application.DTOs.FiltersGroup.Validation
{
    public class FiltersGroupDTOValidator : AbstractValidator<FiltersGroupDTO>
    {
        public FiltersGroupDTOValidator()
        {
            RuleFor(filtersGroup => filtersGroup.Id)
                .GreaterThan(0);

            RuleFor(filtersGroup => filtersGroup.CategoryId)
                .GreaterThan(0);

            RuleFor(filtersGroup => filtersGroup.MinPrice)
                .GreaterThanOrEqualTo(0);

            RuleFor(filtersGroup => filtersGroup.MaxPrice)
                .GreaterThan(filtersGroup => filtersGroup.MinPrice);

            RuleFor(filtersGroup => filtersGroup.AppliedMinPrice)
                .GreaterThanOrEqualTo(filtersGroup => filtersGroup.MinPrice);

            RuleFor(filtersGroup => filtersGroup.AppliedMaxPrice)
                .GreaterThan(filtersGroup => filtersGroup.AppliedMinPrice)
                .LessThanOrEqualTo(filtersGroup => filtersGroup.MaxPrice);
        }
    }
}
