using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.Coupon;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Mapping;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Entities;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    public class CouponsController : BaseController
    {
        private readonly IRepository<Coupon> _repository;

        public CouponsController(IRepository<Coupon> repository) =>
            _repository = repository;

        /// <summary>
        /// Get the enumeration of coupons
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /coupons
        /// </remarks>
        /// <returns>Returns IEnumerable<CouponDTO></returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<CouponDTO>>> GetAll() =>
            Ok((await _repository.GetAllAsync()).ToDTO());

        /// <summary>
        /// Get true if coupon exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /coupons/exists/1
        /// </remarks>
        /// <param name="id">Coupon id</param>
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
        /// Get the coupon by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /coupons/1
        /// </remarks>
        /// <param name="id">Coupon id (int)</param>
        /// <returns>Returns CouponDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet("{id:int}")]
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CouponDTO>> Get(int id) =>
            Ok((await _repository.GetAsync(id)).ToDTO());

        /// <summary>
        /// Create a coupon
        /// </summary>
        /// <remarks>
        /// POST /coupons
        /// {
        ///     name: "Coupon name",
        ///     price: "2155"
        /// }
        /// </remarks>
        /// <param name="createCouponDTO">CreateCouponDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect coupon DTO was passed</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPost]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<int>> Create([FromBody] CreateCouponDTO createCouponDTO)
        {
            var coupon = await _repository.CreateAsync(createCouponDTO.FromDTO());
            if (coupon is null) return UnprocessableEntity();
            return Ok(coupon.Id);
        }

        /// <summary>
        /// Update the coupon
        /// </summary>
        /// <remarks>
        /// PUT /coupons
        /// {
        ///     name: "Updated coupon name"
        /// }
        /// </remarks>
        /// <param name="updateCouponDTO">UpdateCouponDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpPut]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] UpdateCouponDTO updateCouponDTO)
        {
            await _repository.UpdateAsync(updateCouponDTO.FromDTO());
            return NoContent();
        }

        /// <summary>
        /// Delete the coupon by id
        /// </summary>
        /// <remarks>
        /// DELETE /coupons/1
        /// </remarks>
        /// <param name="id">Coupon id (int)</param>
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
