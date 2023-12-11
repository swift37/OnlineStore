using AutoMapper;
using OnlineStore.MVC.Models.Order;
using OnlineStore.MVC.Services.ApiClient;
using OnlineStore.MVC.Services.Base;
using OnlineStore.MVC.Services.Interfaces;

namespace OnlineStore.MVC.Services
{
    public class OrdersService : HttpClientServiceBase, IOrdersService
    {
        public OrdersService(IMapper mapper, IClient client, IHttpContextAccessor httpContextAccessor)
            : base(mapper, client, httpContextAccessor) { }

        public async Task<Response<IEnumerable<OrderViewModel>>> GetAll()
        {
            try
            {
                var orders = await _client.GetAllOrdersAsync(_usingVersion);
                return new Response<IEnumerable<OrderViewModel>>
                {
                    Success = true,
                    Data = _mapper.Map<IEnumerable<OrderViewModel>>(orders)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<IEnumerable<OrderViewModel>>(exception);
            }
        }

        public async Task<Response<OrderViewModel>> Get(int id)
        {
            try
            {
                var order = await _client.GetOrderAsync(id, _usingVersion);
                return new Response<OrderViewModel>
                {
                    Success = true,
                    Data = _mapper.Map<OrderViewModel>(order)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<OrderViewModel>(exception);
            }
        }

        public async Task<Response<bool>> Exist(int id)
        {
            try
            {
                var order = await _client.ExistOrderAsync(id, _usingVersion);
                return new Response<bool>
                {
                    Success = true,
                    Data = _mapper.Map<bool>(order)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<bool>(exception);
            }
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

        public async Task<Response<OrderViewModel>> Get(string number)
        {
            try
            {
                var order = await _client.GetOrderAsync(number, _usingVersion);
                return new Response<OrderViewModel>
                {
                    Success = true,
                    Data = _mapper.Map<OrderViewModel>(order)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<OrderViewModel>(exception);
            }
        }

        public async Task<Response<IEnumerable<OrderViewModel>>> GetUserOrders(Guid userId)
        {
            try
            {
                var orders = await _client.GetUserOrdersAsync(userId, _usingVersion);
                return new Response<IEnumerable<OrderViewModel>>
                {
                    Success = true,
                    Data = _mapper.Map<IEnumerable<OrderViewModel>>(orders)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<IEnumerable<OrderViewModel>>(exception);
            }
        }

        public async Task<Response<IEnumerable<OrderViewModel>>> GetUserOrders()
        {
            try
            {
                var orders = await _client.GetUserOrdersAsync(_usingVersion);
                return new Response<IEnumerable<OrderViewModel>>
                {
                    Success = true,
                    Data = _mapper.Map<IEnumerable<OrderViewModel>>(orders)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<IEnumerable<OrderViewModel>>(exception);
            }
        }

        public async Task<Response<OrderViewModel>> GetUserOrder(int id)
        {
            try
            {
                var order = await _client.GetUserOrderAsync(id, _usingVersion);
                return new Response<OrderViewModel>
                {
                    Success = true,
                    Data = _mapper.Map<OrderViewModel>(order)
                };
            }
            catch (ApiException exception)
            {
                return GenerateResponse<OrderViewModel>(exception);
            }
        }
    }
}
