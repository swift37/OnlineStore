using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.Order;
using OnlineStore.Application.Interfaces.Infrastructure;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Models;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Entities;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    public class OrdersController : BaseController
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly IOrderNumbersProvider _orderNumbersProvider;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public OrdersController(
            IOrdersRepository ordersRepository, 
            IProductsRepository productsRepository,
            IOrderNumbersProvider orderNumbersProvider,
            IPaymentService paymentService,
            IMapper mapper) =>
            (_ordersRepository, _productsRepository, _orderNumbersProvider, _paymentService, _mapper) = 
            (ordersRepository, productsRepository, orderNumbersProvider, paymentService, mapper);

        /// <summary>
        /// Get the enumeration of orders
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /orders
        /// </remarks>
        /// <returns>Returns IEnumerable<OrderDTO></returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAll() =>
            Ok(_mapper.Map<IEnumerable<OrderDTO>>(await _ordersRepository.GetAllAsync()));

        /// <summary>
        /// Get true if order exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /orders/exists/1
        /// </remarks>
        /// <param name="id">Order id</param>
        /// <returns>Returns bool</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet("exists/{id:int}")]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<bool>> Exist(int id) =>
             Ok(await _ordersRepository.ExistsAsync(id));

        /// <summary>
        /// Get the order by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /orders/1
        /// </remarks>
        /// <param name="id">Order id (int)</param>
        /// <returns>Returns OrderDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet("{id:int}")]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<OrderDTO>> Get(int id) =>
            Ok(_mapper.Map<OrderDTO>(await _ordersRepository.GetAsync(id)));

        /// <summary>
        /// Create a order
        /// </summary>
        /// <remarks>
        /// POST /orders
        /// {
        ///     name: "Order name",
        ///     price: "2155"
        /// }
        /// </remarks>
        /// <param name="createOrderDTO">CreateOrderDTO</param>
        /// <returns>Returns order number</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect order DTO was passed</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<string>> Create([FromBody] CreateOrderDTO createOrderDTO)
        {
            var order = _mapper.Map<Order>(createOrderDTO);
            order.UserId = UserId;
            order.Number = await _orderNumbersProvider.GenerateNumberAsync(order);
            order.CreationDate = DateTime.Now;
            foreach (var item in order.Items)
            {
                var product = await _productsRepository.GetAsync(item.ProductId);
                product.UnitsInStock -= item.Quantity;
                await _productsRepository.SaveChangesAsync();

                item.UnitPrice = product.UnitPrice;
                item.Discount = product.Discount;
            }

            var createdOrder = await _ordersRepository.CreateAsync(order);
            if (createdOrder is null) return UnprocessableEntity();
            return Ok(createdOrder.Number);
        }

        /// <summary>
        /// Partially update the order
        /// </summary>
        /// <remarks>
        /// PATCH /orders
        /// {
        ///     id: "1",
        ///     name: "Updated order name"
        /// }
        /// </remarks>
        /// <param name="updateOrderDTO">UpdateOrderDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPatch]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] UpdateOrderDTO updateOrderDTO)
        {
            var order = await _ordersRepository.GetAsync(updateOrderDTO.Id);
            order.Status = updateOrderDTO.Status;
            order.ShippedDate = updateOrderDTO.ShippedDate;
            order.FirstName = updateOrderDTO.FirstName;
            order.LastName = updateOrderDTO.LastName;
            order.Phone = updateOrderDTO.Phone;
            order.Email = updateOrderDTO.Email;
            order.Total = updateOrderDTO.Total;
            order.ShippingCost = updateOrderDTO.ShippingCost;
            order.TrackingNumber = updateOrderDTO.TrackingNumber;
            order.Country = updateOrderDTO.Country;
            order.City = updateOrderDTO.City;
            order.State = updateOrderDTO.State;
            order.Postcode = updateOrderDTO.Postcode;
            order.StreetAddress = updateOrderDTO.StreetAddress;
            order.Apartment = updateOrderDTO.Apartment;
            order.Notes = updateOrderDTO.Notes;

            await _ordersRepository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete the order by id
        /// </summary>
        /// <remarks>
        /// DELETE /orders/1
        /// </remarks>
        /// <param name="id">Order id (int)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(int id)
        {
            await _ordersRepository.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Get the order by number
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /orders/057547564798
        /// </remarks>
        /// <param name="number">Order number</param>
        /// <returns>Returns OrderDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet("{number}")]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<OrderDTO>> Get(string number) =>
            Ok(_mapper.Map<OrderDTO>(await _ordersRepository.GetAsync(number)));

        /// <summary>
        /// Get the orders enumeration by category id
        /// </summary>
        /// <remarks>
        /// GET /orders/user/33CCBEBD-FB8A-4919-B727-B2782502E69E
        /// </remarks>
        /// <param name="userId">User id (Guid)</param>
        /// <returns>Returns IEnumerable<OrderDTO></returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet("user/{userId:Guid}")]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetUserOrders(Guid userId) =>
            Ok(_mapper.Map<IEnumerable<OrderDTO>>(await _ordersRepository.GetUserOrdersAsync(userId)));

        /// <summary>
        /// Get the orders enumeration by category id
        /// </summary>
        /// <remarks>
        /// GET /orders/user/current
        /// </remarks>
        /// <returns>Returns IEnumerable<OrderDTO></returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("user/current")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetUserOrders() => 
            Ok(_mapper.Map<IEnumerable<OrderDTO>>(await _ordersRepository.GetUserOrdersAsync(UserId)));

        /// <summary>
        /// Get the order belonging to the current user by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /orders/user/current/1
        /// </remarks>
        /// <param name="id">Order id (int)</param>
        /// <returns>Returns OrderDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("user/current/{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<OrderDTO>> GetUserOrder(int id) => 
            Ok(_mapper.Map<OrderDTO>(await _ordersRepository.GetUserOrderAsync(id, UserId)));

        /// <summary>
        /// Get the current user orders with products awaiting reviews
        /// </summary>
        /// <remarks>
        /// GET /orders/user/current/awaiting-reviews
        /// </remarks>
        /// <returns>Returns IEnumerable<OrderDTO></returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("user/current/awaiting-reviews")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetUserOrdersAwaitingReview() =>
            Ok(_mapper.Map<IEnumerable<Order>>(await _ordersRepository.GetUserOrdersAwaitingReviewAsync(UserId)));

        /// <summary>
        /// Create a Stripe payment session for the order
        /// </summary>
        /// <remarks>
        /// POST /orders/payment/stripe
        /// {
        ///     orderNumber: "098734573475",
        ///     successUrl: "https://onlinestore.com/payment/success",
        ///     cancelUrl: "https://onlinestore.com/payment/cancel"
        /// }
        /// </remarks>
        /// <param name="stripePaymentRequest">StripePaymentRequest</param>
        /// <returns>Returns PaymentSessionResponse</returns>
        /// <response code="200">Success</response>
        [HttpPost("payment/stripe")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PaymentSessionResponse>> StripePayment(StripePaymentRequest stripePaymentRequest) =>
            Ok(await _paymentService.StripePayment(stripePaymentRequest));

        /// <summary>
        /// Confirm order payment via Stripe
        /// </summary>
        /// <remarks>
        /// GET /orders/payment/stripe/confirm
        /// {
        ///     orderNumber: "098734573475",
        /// }
        /// </remarks>
        /// <param name="orderNumber">Order number</param>
        /// <returns>Returns PaymentStatusResponse</returns>
        /// <response code="200">Success</response>
        [HttpGet("payment/stripe/confirm")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PaymentStatusResponse>> ConfirmStripePayment(string orderNumber) => 
            Ok(await _paymentService.ConfirmStripePayment(orderNumber));

        ///// <summary>
        ///// Check availability of items in the order
        ///// </summary>
        ///// <remarks>
        ///// Sample request:
        ///// GET /availability/057547564798
        ///// </remarks>
        ///// <param name="number">Order number</param>
        ///// <returns>Returns OrderDTO</returns>
        ///// <response code="200">Success</response>
        //[HttpGet("availability/{number}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<ActionResult<bool>> CheckGoodsAvailability(string number)
        //{
        //    var order = await _ordersRepository.GetAsync(number);
        //}
    }
}
