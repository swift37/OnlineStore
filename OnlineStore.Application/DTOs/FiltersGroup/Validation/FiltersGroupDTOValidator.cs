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
        }
    }
}
