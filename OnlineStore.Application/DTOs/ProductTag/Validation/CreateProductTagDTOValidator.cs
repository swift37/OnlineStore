using FluentValidation;

namespace OnlineStore.Application.DTOs.ProductTag.Validation
{
    public class CreateProductTagDTOValidator : AbstractValidator<CreateProductTagDTO>
    {
        public CreateProductTagDTOValidator()
        {
            RuleFor(pt => pt.Name)
                .MaximumLength(32);

            RuleFor(pt => pt.DisplayName)
                .MaximumLength(32);

            RuleFor(pt => pt.ColorHex)
                .MaximumLength(7);
        }
    }
}
