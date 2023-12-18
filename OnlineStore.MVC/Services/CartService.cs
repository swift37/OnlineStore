using OnlineStore.MVC.Models.Cart;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Services
{
    public class CartService : ICartService
    {
        private readonly ICartStorage _cartStore;
        private readonly IProductsService _productsService;

        public CartService(ICartStorage cartStore, IProductsService productsService) =>
            (_cartStore, _productsService) = (cartStore, productsService);

        public bool Add(int productId, int quantity = 1)
        {
            var cart = _cartStore.Cart;
            var item = cart?.Items.FirstOrDefault(i => i.ProductId == productId);

            if (item is null)
            {
                var response = _productsService.Exist(productId).Result;
                if (!response.Success) return false;

                cart?.Items.Add(new CartItemViewModel
                {
                    ProductId = productId,
                    Quantity = quantity
                });
            }
            else
                item.Quantity += quantity;

            _cartStore.Cart = cart;
            return true;
        }

        public bool Update(int productId, int quantity)
        {
            var cart = _cartStore.Cart;
            var item = cart?.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item is null) return false;

            if (item.Quantity > 0 && quantity > 0 && quantity < 1000)
                item.Quantity = quantity;

            _cartStore.Cart = cart;
            return true;
        }

        public bool Remove(int productId)
        {
            var cart = _cartStore.Cart;
            var item = cart?.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item is null) return false;

            cart?.Items.Remove(item);

            _cartStore.Cart = cart;
            return true;
        }

        public void Clear()
        {
            var cart = _cartStore.Cart;
            cart?.Items.Clear();
            _cartStore.Cart = cart;
        }
    }
}
