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


    public class CreateWishlistItemDTOValidator : AbstractValidator<CreateWishlistItemDTO>
    {
        public CreateWishlistItemDTOValidator()
        {
            RuleFor(i => i.ProductId)
                .GreaterThan(0);

            RuleFor(i => i.Quantity)
                .GreaterThan(0);
        }
    }
}
