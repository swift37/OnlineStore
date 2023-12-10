using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.Order;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Mapping;
using OnlineStore.Domain.Constants;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    public class OrdersController : BaseController
    {
        private readonly IOrdersRepository _repository;

        public OrdersController(IOrdersRepository repository) =>
            _repository = repository;

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
            Ok((await _repository.GetAllAsync()).ToDTO());

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
             Ok(await _repository.ExistsAsync(id));

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
            Ok((await _repository.GetAsync(id)).ToDTO());

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
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect order DTO was passed</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<int>> Create([FromBody] CreateOrderDTO createOrderDTO)
        {
            var order = createOrderDTO.FromDTO();
            order.UserId = UserId;

            var createdOrder = await _repository.CreateAsync(order);
            if (createdOrder is null) return UnprocessableEntity();
            return Ok(createdOrder.Id);
        }

        /// <summary>
        /// Update the order
        /// </summary>
        /// <remarks>
        /// PUT /orders
        /// {
        ///     name: "Updated order name"
        /// }
        /// </remarks>
        /// <param name="updateOrderDTO">UpdateOrderDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPut]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] UpdateOrderDTO updateOrderDTO)
        {
            await _repository.UpdateAsync(updateOrderDTO.FromDTO());
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
            await _repository.DeleteAsync(id);
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
            Ok((await _repository.GetAsync(number)).ToDTO());

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
            Ok((await _repository.GetUserOrdersAsync(userId)).ToDTO());

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
            Ok((await _repository.GetUserOrdersAsync(UserId)).ToDTO());

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
            Ok((await _repository.GetUserOrderAsync(id, UserId)).ToDTO());
    }
}
