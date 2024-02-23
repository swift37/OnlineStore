using OnlineStore.Application.Interfaces.Infrastructure;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Models;
using OnlineStore.Domain.Enums;
using Stripe.Checkout;

namespace OnlineStore.Application.Infrastructure
{
    public class PaymentService : IPaymentService
    {
        private readonly IOrdersRepository _ordersRepository;

        public PaymentService(IOrdersRepository ordersRepository) => 
            _ordersRepository = ordersRepository;

        public async Task<PaymentSessionResponse> StripePayment(StripePaymentRequest stripePaymentRequest)
        {
            var order = await _ordersRepository.GetAsync(stripePaymentRequest.OrderNumber);

            if (order.Status is not OrderStatus.NotPaid)
                throw new Exception("The order has already been paid.");

            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                PaymentMethodTypes = new List<string> { "card" },
                SuccessUrl = stripePaymentRequest.SuccessUrl,
                CancelUrl = stripePaymentRequest.CancelUrl
            };

            foreach (var item in order.Items)
            {
                if (item.Product is null)
                    throw new MissingMemberException("One of the ordered products has been missed during payment preparation. The operation has been canceled.");

                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmountDecimal = (item.Product.UnitPrice - item.Discount) * 100,
                        Currency = "USD",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Name,
                        }
                    },
                    Quantity = item.Quantity
                });
            }

            var service = new SessionService();
            Session session = service.Create(options);

            order.PaymentMethod = "Stripe";
            order.PaymentSession = session.Id;
            await _ordersRepository.SaveChangesAsync();

            return new PaymentSessionResponse { SessionUrl = session.Url };
        }

        public async Task<PaymentStatusResponse> ConfirmStripePayment(string orderNumber)
        {
            var order = await _ordersRepository.GetAsync(orderNumber);

            if (order.Status is not OrderStatus.NotPaid)
                return new PaymentStatusResponse { OrderNumber = order.Number!, IsPaid = true };

            if (order.PaymentMethod?.ToLower() != "stripe")
                throw new Exception("Payment method is not 'Stripe'.");

            var sessionService = new SessionService();
            var session = sessionService.Get(order.PaymentSession);

            if (session.Status == "open")
                return new PaymentStatusResponse { OrderNumber = order.Number!, IsInProcess = true };

            if (session.PaymentStatus != "paid")
                return new PaymentStatusResponse { OrderNumber = order.Number!, IsFailed = true };

            // Saving order and customer details to the database.
            var total = session.AmountTotal.HasValue ? session.AmountTotal.Value : 0;
            var customerEmail = session.CustomerDetails.Email;
            var payDate = session.Created;

            order.Status = OrderStatus.Paid;
            order.Total = (decimal)total / 100;
            order.Email = customerEmail;
            order.PayDate = payDate;

            await _ordersRepository.SaveChangesAsync();

            return new PaymentStatusResponse { OrderNumber = order.Number!, IsPaid = true };
        }
    }
}
