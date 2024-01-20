using FluentValidation;

namespace OnlineStore.Application.DTOs.Wishlist.Validation
{
    public class UpdateWishlistDTOValidator : AbstractValidator<UpdateWishlistDTO>
    {
        public UpdateWishlistDTOValidator()
        {
            RuleFor(w => w.Id)
                .GreaterThan(0);

            RuleFor(w => w.LastChangeDate)
                .NotEqual(default(DateTime));
        }
    }


    public class UpdateWishlistItemDTOValidator : AbstractValidator<UpdateWishlistItemDTO>
    {
        public UpdateWishlistItemDTOValidator()
        {
            RuleFor(i => i.Id)
                .GreaterThan(0);

            RuleFor(i => i.ProductId)
                .GreaterThan(0);

            RuleFor(i => i.Quantity)
                .GreaterThan(0);
        }
    }
}
