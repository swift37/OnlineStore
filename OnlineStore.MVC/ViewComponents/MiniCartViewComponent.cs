using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.ViewComponents
{
    public class MiniCartViewComponent : ViewComponent
    {
        private readonly ICartStorage _cartStorage;

        public MiniCartViewComponent(ICartStorage cartStorage) => _cartStorage = cartStorage;

        public Task<IViewComponentResult> InvokeAsync() => 
            Task.FromResult<IViewComponentResult>(View(_cartStorage.Cart)); 
    }
}
