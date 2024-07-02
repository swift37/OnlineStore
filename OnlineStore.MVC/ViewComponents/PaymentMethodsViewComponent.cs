using Microsoft.AspNetCore.Mvc;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.ViewComponents
{
    public class PaymentMethodsViewComponent : ViewComponent
    {
        private readonly IPaymentMethodsService _paymentMethodsService;

        public PaymentMethodsViewComponent(IPaymentMethodsService paymentMethodsService) =>
            _paymentMethodsService = paymentMethodsService;

        public async Task<IViewComponentResult> InvokeAsync(int productId, string? text)
        {
            var response = await _paymentMethodsService.GetAll();
            var result = response.Data.Where(method => method.IsAvailable);

            return View(result);
        }
    }
}
