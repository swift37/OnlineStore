using FluentValidation;

namespace OnlineStore.Application.DTOs.Wishlist.Validation
{
    public class CreateWishlistDTOValidator : AbstractValidator<CreateWishlistDTO>
    {
        public CreateWishlistDTOValidator()
        {
            RuleFor(w => w.CreateDate)
                .NotEqual(default(DateTime));

            RuleFor(w => w.LastChangeDate)
                .NotEqual(default(DateTime));
        }
    }
}
