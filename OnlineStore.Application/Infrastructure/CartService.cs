using OnlineStore.Application.Interfaces.Infrastructure;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain;

namespace OnlineStore.Application.Infrastructure
{
    public class CartService : ICartService
    {
        private readonly ICartStore _cartStore;
        private readonly IProductsRepository _productsRepository;

        public CartService(ICartStore cartStore, IProductsRepository productsRepository) => 
            (_cartStore, _productsRepository) = (cartStore, productsRepository);

        public void Add(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart?.Items.FirstOrDefault(i => i.Product?.Id == id);

            if (item is null)
            {
                var newItem = _productsRepository.Get(id).Result;
                cart?.Items.Add(new CartItem { Product = newItem, Quantity = 1 });
            }
            else
                item.Quantity++;

            _cartStore.Cart = cart;
        }

        public void Decrement(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart?.Items.FirstOrDefault(i => i.Product?.Id == id);
            if (item is null) return;

            if (item.Quantity > 0)
                item.Quantity--;

            if (item.Quantity == 0)
                cart?.Items.Remove(item);

            _cartStore.Cart = cart;
        }

        public void Remove(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart?.Items.FirstOrDefault(i => i.Product?.Id == id);
            if (item is null) return;

            cart?.Items.Remove(item);

            _cartStore.Cart = cart;
        }

        public void Clear()
        {
            var cart = _cartStore.Cart;
            cart?.Items.Clear();
            _cartStore.Cart = cart;
        }
    }
}
