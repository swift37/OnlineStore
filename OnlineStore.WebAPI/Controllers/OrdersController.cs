using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Mapping;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// <response code="404">Not Found</response>
        [HttpGet("exists/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> Exist(int id) =>
            await _repository.ExistsAsync(id) ? Ok(true) : NotFound(false);

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
        /// <response code="404">Not Found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDTO>> Get(int id)
        {
            var order = await _repository.GetAsync(id);
            if (order is null) return NotFound();

            return Ok(order.ToDTO());
        }

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
        /// <param name="orderDTO">OrderDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect order DTO was passed</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<int>> Create([FromBody] OrderDTO orderDTO)
        {
            var order = await _repository.CreateAsync(orderDTO.FromDTO());
            if (order is null) return UnprocessableEntity();
            return Ok(order.Id);
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
        /// <param name="orderDTO">OrderDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromBody] OrderDTO orderDTO)
        {
            await _repository.UpdateAsync(orderDTO.FromDTO());
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
        /// <response code="404">Not Found</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id) =>
            await _repository.DeleteAsync(id) ? NoContent() : NotFound();

        /// <summary>
        /// Get the orders enumeration by category id
        /// </summary>
        /// <remarks>
        /// GET /orders/user/33CCBEBD-FB8A-4919-B727-B2782502E69E
        /// </remarks>
        /// <param name="userId">User id (Guid)</param>
        /// <returns>Returns IEnumerable<OrderDTO></returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("user/{userId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetUserOrders(Guid userId)
        {
            var orders = await _repository.GetUserOrdersAsync(userId);

            if (orders == null || orders.Count() < 1) return NotFound();

            return Ok(orders.ToDTO());
        }

        /// <summary>
        /// Get the orders enumeration by category id
        /// </summary>
        /// <remarks>
        /// GET /orders/user/current
        /// </remarks>
        /// <returns>Returns IEnumerable<OrderDTO></returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("user/current")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetUserOrders()
        {
            var orders = await _repository.GetUserOrdersAsync(UserId);

            if (orders == null || orders.Count() < 1) return NotFound();

            return Ok(orders.ToDTO());
        }

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
        /// <response code="404">Not Found</response>
        [HttpGet("user/current/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDTO>> GetUserOrder(int id)
        {
            var order = await _repository.GetUserOrderAsync(id, UserId);

            if (order is null) return NotFound();

            return Ok(order.ToDTO());
        }
    }
}
