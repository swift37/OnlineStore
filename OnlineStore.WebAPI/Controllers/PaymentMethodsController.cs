using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.PaymentMethod;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Entities;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/payment-methods")]
    public class PaymentMethodsController : BaseController
    {
        private readonly IRepository<PaymentMethod> _repository;

        private readonly IMapper _mapper;

        public PaymentMethodsController(IRepository<PaymentMethod> repository, IMapper mapper) =>
            (_repository, _mapper) = (repository, mapper);

        /// <summary>
        /// Get the enumeration of payment methods
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /payment-methods
        /// </remarks>
        /// <returns>Returns IEnumerable<PaymentMethodDTO></returns>
        /// <response code="200">Success</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<PaymentMethodDTO>>> GetAll() =>
            Ok(_mapper.Map<IEnumerable<PaymentMethodDTO>>(await _repository.GetAllAsync()));

        /// <summary>
        /// Get true if payment method exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /payment-methods/exists/1
        /// </remarks>
        /// <param name="id">PaymentMethod id</param>
        /// <returns>Returns bool</returns>
        /// <response code="200">Success</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet("exists/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<bool>> Exist(int id) =>
            Ok(await _repository.ExistsAsync(id));

        /// <summary>
        /// Get the payment method by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /payment-methods/1
        /// </remarks>
        /// <param name="id">PaymentMethod id (int)</param>
        /// <returns>Returns PaymentMethodDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PaymentMethodDTO>> Get(int id) =>
            Ok(_mapper.Map<PaymentMethodDTO>(await _repository.GetAsync(id)));

        /// <summary>
        /// Create a payment method
        /// </summary>
        /// <remarks>
        /// POST /payment-methods
        /// {
        ///     name: "PaymentMethod name",
        ///     colorHex: "#fff"
        /// }
        /// </remarks>
        /// <param name="createPaymentMethodDTO">CreatePaymentMethodDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="422">If the incorrect paymentMethod DTO was passed</response>
        [HttpPost]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<int>> Create([FromBody] CreatePaymentMethodDTO createPaymentMethodDTO)
        {
            var paymentMethod = _mapper.Map<PaymentMethod>(createPaymentMethodDTO);

            if (await _repository.CreateAsync(paymentMethod) is null)
                return UnprocessableEntity();

            return Ok(paymentMethod.Id);
        }

        /// <summary>
        /// Partially update the payment method
        /// </summary>
        /// <remarks>
        /// PATCH /payment-methods
        /// {
        ///     id: "1",
        ///     name: "Updated payment method name"
        /// }
        /// </remarks>
        /// <param name="updatePaymentMethodDTO">UpdatePaymentMethodDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPatch]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] UpdatePaymentMethodDTO updatePaymentMethodDTO)
        {
            var paymentMethod = await _repository.GetAsync(updatePaymentMethodDTO.Id);
            paymentMethod.Name = paymentMethod.Name;
            paymentMethod.Image = paymentMethod.Image;
            paymentMethod.IsAvailable = paymentMethod.IsAvailable;

            await _repository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete the payment method by id
        /// </summary>
        /// <remarks>
        /// DELETE /payment-methods/1
        /// </remarks>
        /// <param name="id">PaymentMethod id (int)</param>
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
