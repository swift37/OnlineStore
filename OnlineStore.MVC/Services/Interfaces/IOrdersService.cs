using OnlineStore.MVC.Models.Order;
using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Services.Interfaces
{
    public interface IOrdersService
    {
        Task<IEnumerable<OrderViewModel>> GetAll();

        Task<bool> Exist(int id);

        Task<OrderViewModel> Get(int id);

        Task<Response<int>> Create(CreateOrderViewModel createOrderViewModel);

        Task<Response> Update(OrderViewModel orderViewModel);

        Task<Response> Delete(int id);

        Task<IEnumerable<OrderViewModel>> GetUserOrders(Guid userId);

        Task<IEnumerable<OrderViewModel>> GetUserOrders();

        Task<OrderViewModel> GetUserOrder(int id);
    }
}
