using OnlineStore.Application.Models;

namespace OnlineStore.Application.Interfaces.Infrastructure
{
    public interface IPaymentService
    {
        Task<PaymentSessionResponse> StripePayment(StripePaymentRequest stripePaymentRequest);

        Task<PaymentStatusResponse> ConfirmStripePayment(string orderNumber);
    }
}
