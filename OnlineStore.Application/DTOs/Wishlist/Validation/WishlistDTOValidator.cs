using FluentValidation;

namespace OnlineStore.Application.DTOs.Wishlist.Validation
{
    public class WishlistDTOValidator : AbstractValidator<WishlistDTO>
    {
        public WishlistDTOValidator()
        {
            RuleFor(w => w.Id)
                .GreaterThan(0);

            RuleFor(w => w.CreateDate)
                .NotEqual(default(DateTime));

            RuleFor(w => w.LastChangeDate)
                .NotEqual(default(DateTime));
        }
    }
}
