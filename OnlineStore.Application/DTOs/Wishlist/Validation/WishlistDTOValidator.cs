using FluentValidation;

namespace OnlineStore.Application.DTOs.Wishlist.Validation
{
    public class WishlistDTOValidator : AbstractValidator<WishlistDTO>
    {
        public WishlistDTOValidator()
        {
            RuleFor(w => w.Id)
                .GreaterThan(0);

            RuleFor(w => w.CreationDate)
                .NotEqual(default(DateTime));

            RuleFor(w => w.LastChangeDate)
                .NotEqual(default(DateTime));
        }
    }

    public class WishlistItemDTOValidator : AbstractValidator<WishlistItemDTO>
    {
        public WishlistItemDTOValidator()
        {
            RuleFor(i => i.Id)
                .GreaterThan(0);

            RuleFor(i => i.WishlistId)
                .GreaterThan(0);

            RuleFor(i => i.ProductId)
                .GreaterThan(0);

            RuleFor(i => i.Quantity)
                .GreaterThan(0);
        }
    }
}
