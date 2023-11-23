using FluentValidation;

namespace OnlineStore.Application.DTOs.Wishlist.Validation
{
    public class UpdateWishlistDTOValidator : AbstractValidator<UpdateWishlistDTO>
    {
        public UpdateWishlistDTOValidator()
        {
            RuleFor(w => w.Id)
                .NotEqual(0);

            RuleFor(w => w.LastChangeDate)
                .NotEqual(default(DateTime));
        }
    }
}
