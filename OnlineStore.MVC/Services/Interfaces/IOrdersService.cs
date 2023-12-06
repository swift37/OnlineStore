using OnlineStore.MVC.Models.Order;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface IOrdersService
    {
        Task<Response<IEnumerable<OrderViewModel>>> GetAll();

        Task<Response<bool>> Exist(int id);

        Task<Response<OrderViewModel>> Get(int id);

        Task<Response<int>> Create(CreateOrderViewModel createOrderViewModel);

        Task<Response> Update(OrderViewModel orderViewModel);

        Task<Response> Delete(int id);

        Task<Response<IEnumerable<OrderViewModel>>> GetUserOrders(Guid userId);

        Task<Response<IEnumerable<OrderViewModel>>> GetUserOrders();

        Task<Response<OrderViewModel>> GetUserOrder(int id);
    }
}
