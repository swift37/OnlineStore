using FluentValidation;

namespace OnlineStore.Application.DTOs.FiltersGroup.Validation
{
    public class CreateFiltersGroupDTOValidator : AbstractValidator<CreateFiltersGroupDTO>
    {
        public CreateFiltersGroupDTOValidator()
        {
            RuleFor(filtersGroup => filtersGroup.CategoryId)
                .GreaterThan(0);
        }
    }
}
