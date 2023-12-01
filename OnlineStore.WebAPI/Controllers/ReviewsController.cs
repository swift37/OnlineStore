﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.Review;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Mapping;
using OnlineStore.Domain.Constants;
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
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet("exists/{id:int}")]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<bool>> Exist(int id) => 
            Ok(await _repository.ExistsAsync(id));

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
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet("{id:int}")]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ReviewDTO>> Get(int id) => 
            Ok((await _repository.GetAsync(id)).ToDTO());

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
        /// <param name="createReviewDTO">CreateReviewDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect review DTO was passed</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> Create([FromBody] CreateReviewDTO createReviewDTO)
        {
            var review = await _repository.CreateAsync(createReviewDTO.FromDTO());
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
        /// <param name="updateReviewDTO">UpdateReviewDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromBody] UpdateReviewDTO updateReviewDTO)
        {
            await _repository.UpdateAsync(updateReviewDTO.FromDTO());
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
        /// Get the reviews enumeration by category id
        /// </summary>
        /// <remarks>
        /// GET /reviews/product/1
        /// </remarks>
        /// <param name="productId">Product id (int)</param>
        /// <returns>Returns IEnumerable<ReviewDTO></returns>
        /// <response code="200">Success</response>
        [HttpGet("product/{productId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviewsByProduct(int productId) => 
            Ok((await _repository.GetReviewsByProductAsync(productId)).ToDTO());
    }
}
