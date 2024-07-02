using FluentValidation;

namespace OnlineStore.Application.DTOs.FiltersGroup.Validation
{
    public class UpdateFiltersGroupDTOValidator : AbstractValidator<UpdateFiltersGroupDTO>
    {
        public UpdateFiltersGroupDTOValidator()
        {
            RuleFor(filtersGroup => filtersGroup.Id)
                .GreaterThan(0);

            RuleFor(filtersGroup => filtersGroup.CategoryId)
                .GreaterThan(0);
        }
    }
}
