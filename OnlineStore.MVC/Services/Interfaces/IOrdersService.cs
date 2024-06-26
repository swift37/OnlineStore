﻿using OnlineStore.MVC.Models;
using OnlineStore.MVC.Models.Order;
using OnlineStore.MVC.Models.Subscriber;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface IOrdersService
    {
        Task<Response<IEnumerable<OrderViewModel>>> GetAll();

        Task<Response<bool>> Exist(int id);

        Task<Response<OrderViewModel>> Get(int id);

        Task<Response<string>> Create(CreateOrderViewModel createOrderViewModel);

        Task<Response> Update(OrderViewModel orderViewModel);

        Task<Response> Delete(int id);

        Task<Response<OrderViewModel>> Get(string number);

        Task<Response<IEnumerable<OrderViewModel>>> GetUserOrders(Guid userId);

        Task<Response<IEnumerable<OrderViewModel>>> GetUserOrders();

        Task<Response<OrderViewModel>> GetUserOrder(int id);

        Task<Response<IEnumerable<OrderViewModel>>> GetUserOrdersAwaitingReview();

        Task<Response<PaymentSessionResponse>> StripePayment(StripePaymentRequest requestModel);

        Task<Response<PaymentStatusResponse>> ConfirmStripePayment(string orderNumber);
    }
}
