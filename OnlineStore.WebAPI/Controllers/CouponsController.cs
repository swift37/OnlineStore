using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Mapping;
using OnlineStore.Domain;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// <response code="404">Not Found</response>
        [HttpGet("exists/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> Exist(int id) =>
            await _repository.ExistsAsync(id) ? Ok(true) : NotFound(false);

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
        /// <response code="404">Not Found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CouponDTO>> Get(int id)
        {
            var coupon = await _repository.GetAsync(id);
            if (coupon is null) return NotFound();

            return Ok(coupon.ToDTO());
        }

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
        /// <param name="couponDTO">CouponDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect coupon DTO was passed</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<int>> Create([FromBody] CouponDTO couponDTO)
        {
            var coupon = await _repository.CreateAsync(couponDTO.FromDTO());
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
        /// <param name="couponDTO">CouponDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromBody] CouponDTO couponDTO)
        {
            await _repository.UpdateAsync(couponDTO.FromDTO());
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
        /// <response code="404">Not Found</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id) =>
            await _repository.DeleteAsync(id) ? NoContent() : NotFound();
    }
}
