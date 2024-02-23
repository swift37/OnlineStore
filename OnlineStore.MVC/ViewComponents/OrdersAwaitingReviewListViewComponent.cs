using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.ViewComponents
{
    public class OrdersAwaitingReviewListViewComponent : ViewComponent
    {
        private readonly IOrdersService _ordersService;

        public OrdersAwaitingReviewListViewComponent(IOrdersService ordersService) =>
            _ordersService = ordersService;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _ordersService.GetUserOrdersAwaitingReview();
            var model = response.Data;
            return View(model);
        }
    }
}
