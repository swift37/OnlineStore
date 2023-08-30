using OnlineStore.Domain.Base;

namespace OnlineStore.Domain
{
    public class CartItem : Entity
    {
        public int ProductId { get; set; }

        public Product? Product { get; set; }

        public int Quantity { get; set; }

        public decimal Price => Product?.PriceBeforeDiscount * Quantity ?? default;
    }
}
