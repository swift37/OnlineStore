using FluentValidation;
using OnlineStore.Domain;

namespace OnlineStore.Application.DTOs.FiltersGroup.Validation
{
    public class FiltersGroupOptionsValidator : AbstractValidator<FiltersGroupOptions>
    {
        public FiltersGroupOptionsValidator()
        {
            RuleFor(opt => opt.CategoryId)
                .GreaterThan(0);
        }
    }
}
