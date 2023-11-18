using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Mapping;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
    [Produces("application/json")]
    public class ReviewsController : BaseController
    {
        private readonly IReviewsRepository _repository;

        public ReviewsController(IReviewsRepository repository) =>
            _repository = repository;

        /// <summary>
        /// Get the enumeration of reviews
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /reviews
        /// </remarks>
        /// <returns>Returns IEnumerable<ReviewDTO></returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetAll() =>
            Ok((await _repository.GetAllAsync()).ToDTO());

        /// <summary>
        /// Get true if review exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /reviews/exists/1
        /// </remarks>
        /// <param name="id">Review id</param>
        /// <returns>Returns bool</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("exists/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> Exist(int id) =>
            await _repository.ExistsAsync(id) ? Ok(true) : NotFound(false);

        /// <summary>
        /// Get the review by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /reviews/1
        /// </remarks>
        /// <param name="id">Review id (int)</param>
        /// <returns>Returns ReviewDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReviewDTO>> Get(int id)
        {
            var review = await _repository.GetAsync(id);
            if (review is null) return NotFound();

            return Ok(review.ToDTO());
        }

        /// <summary>
        /// Create a review
        /// </summary>
        /// <remarks>
        /// POST /reviews
        /// {
        ///     name: "Review name",
        ///     price: "2155"
        /// }
        /// </remarks>
        /// <param name="reviewDTO">ReviewDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect review DTO was passed</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<int>> Create([FromBody] ReviewDTO reviewDTO)
        {
            var review = await _repository.CreateAsync(reviewDTO.FromDTO());
            if (review is null) return UnprocessableEntity();
            return Ok(review.Id);
        }

        /// <summary>
        /// Update the review
        /// </summary>
        /// <remarks>
        /// PUT /reviews
        /// {
        ///     name: "Updated review name"
        /// }
        /// </remarks>
        /// <param name="reviewDTO">ReviewDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromBody] ReviewDTO reviewDTO)
        {
            await _repository.UpdateAsync(reviewDTO.FromDTO());
            return NoContent();
        }

        /// <summary>
        /// Delete the review by id
        /// </summary>
        /// <remarks>
        /// DELETE /reviews/1
        /// </remarks>
        /// <param name="id">Review id (int)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not Found</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id) =>
            await _repository.DeleteAsync(id) ? NoContent() : NotFound();

        /// <summary>
        /// Get the reviews enumeration by category id
        /// </summary>
        /// <remarks>
        /// GET /reviews/product/1
        /// </remarks>
        /// <param name="productId">Product id (int)</param>
        /// <returns>Returns IEnumerable<ReviewDTO></returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("product/{productId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviewsByProduct(int productId)
        {
            var reviews = await _repository.GetReviewsByProductAsync(productId);

            if (reviews == null || reviews.Count() < 1) return NotFound();

            return Ok(reviews.ToDTO());
        }
    }
}
