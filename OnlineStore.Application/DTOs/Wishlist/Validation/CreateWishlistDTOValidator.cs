using FluentValidation;

namespace OnlineStore.Application.DTOs.Wishlist.Validation
{
    public class CreateWishlistDTOValidator : AbstractValidator<CreateWishlistDTO>
    {
        public CreateWishlistDTOValidator() { }
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
