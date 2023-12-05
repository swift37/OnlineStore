using AutoMapper;
using OnlineStore.MVC.Models.Order;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Services
{
    public class OrdersService : HttpClientServiceBase, IOrdersService
    {
        public OrdersService(IMapper mapper, IClient client) : base(mapper, client) { }

        public Task<IEnumerable<OrderViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OrderViewModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exist(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<int>> Create(CreateOrderViewModel createOrderViewModel)
        {
            var createOrderDTO = _mapper.Map<CreateOrderDTO>(createOrderViewModel);

            try
            {
                var response = await _client.CreateOrderAsync(_usingVersion, createOrderDTO);
                return new Response<int>
                {
                    Success = true,
                    Data = response
                };
            }
            catch (ApiException e)
            {
                var generatedResponse = GenerateResponse(e);
                return new Response<int>(generatedResponse);
            }
        }

        public async Task<Response> Update(OrderViewModel orderViewModel)
        {
            var updateOrderDTO = _mapper.Map<UpdateOrderDTO>(orderViewModel);

            try
            {
                await _client.UpdateOrderAsync(_usingVersion, updateOrderDTO);
                return new Response { Success = true };
            }
            catch (ApiException e)
            {
                return GenerateResponse(e);
            }
        }

        public async Task<Response> Delete(int id)
        {
            try
            {
                await _client.DeleteOrderAsync(id, _usingVersion);
                return new Response { Success = true };
            }
            catch (ApiException e)
            {
                return GenerateResponse(e);
            }
        }

        public Task<IEnumerable<OrderViewModel>> GetUserOrders(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderViewModel>> GetUserOrders()
        {
            throw new NotImplementedException();
        }

        public Task<OrderViewModel> GetUserOrder(int id)
        {
            throw new NotImplementedException();
        }
    }
}
