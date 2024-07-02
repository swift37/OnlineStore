using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.ShippingMethod;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Entities;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/shipping-methods")]
    public class ShippingMethodsController : BaseController
    {
        private readonly IRepository<ShippingMethod> _repository;

        private readonly IMapper _mapper;

        public ShippingMethodsController(IRepository<ShippingMethod> repository, IMapper mapper) =>
            (_repository, _mapper) = (repository, mapper);

        /// <summary>
        /// Get the enumeration of shipping methods
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /shipping-methods
        /// </remarks>
        /// <returns>Returns IEnumerable<ShippingMethodDTO></returns>
        /// <response code="200">Success</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<ShippingMethodDTO>>> GetAll() =>
            Ok(_mapper.Map<IEnumerable<ShippingMethodDTO>>(await _repository.GetAllAsync()));

        /// <summary>
        /// Get true if shipping method exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /shipping-methods/exists/1
        /// </remarks>
        /// <param name="id">ShippingMethod id</param>
        /// <returns>Returns bool</returns>
        /// <response code="200">Success</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet("exists/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<bool>> Exist(int id) =>
            Ok(await _repository.ExistsAsync(id));

        /// <summary>
        /// Get the shipping method by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /shipping-methods/1
        /// </remarks>
        /// <param name="id">ShippingMethod id (int)</param>
        /// <returns>Returns ShippingMethodDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ShippingMethodDTO>> Get(int id) =>
            Ok(_mapper.Map<ShippingMethodDTO>(await _repository.GetAsync(id)));

        /// <summary>
        /// Create a shipping method
        /// </summary>
        /// <remarks>
        /// POST /shipping-methods
        /// {
        ///     name: "ShippingMethod name",
        ///     colorHex: "#fff"
        /// }
        /// </remarks>
        /// <param name="createShippingMethodDTO">CreateShippingMethodDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="422">If the incorrect shippingMethod DTO was passed</response>
        [HttpPost]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<int>> Create([FromBody] CreateShippingMethodDTO createShippingMethodDTO)
        {
            var shippingMethod = _mapper.Map<ShippingMethod>(createShippingMethodDTO);

            if (await _repository.CreateAsync(shippingMethod) is null)
                return UnprocessableEntity();

            return Ok(shippingMethod.Id);
        }

        /// <summary>
        /// Partially update the shipping method
        /// </summary>
        /// <remarks>
        /// PATCH /shipping-methods
        /// {
        ///     id: "1",
        ///     name: "Updated shipping method name"
        /// }
        /// </remarks>
        /// <param name="updateShippingMethodDTO">UpdateShippingMethodDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPatch]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] UpdateShippingMethodDTO updateShippingMethodDTO)
        {
            var shippingMethod = await _repository.GetAsync(updateShippingMethodDTO.Id);
            shippingMethod.Name = shippingMethod.Name;
            shippingMethod.Price = shippingMethod.Price;
            shippingMethod.Image = shippingMethod.Image;
            shippingMethod.IsAvailable = shippingMethod.IsAvailable;

            await _repository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete the shipping method by id
        /// </summary>
        /// <remarks>
        /// DELETE /shipping-methods/1
        /// </remarks>
        /// <param name="id">ShippingMethod id (int)</param>
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
    }
}
