using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.Review;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Constants;
using OnlineStore.Domain.Entities;
using OnlineStore.WebAPI.Controllers.Base;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OnlineStore.WebAPI.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    public class ReviewsController : BaseController
    {
        private readonly IReviewsRepository _repository;

        private readonly IMapper _mapper;

        public ReviewsController(IReviewsRepository repository, IMapper mapper) =>
            (_repository, _mapper) = (repository, mapper);

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
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetAll() =>
            Ok(_mapper.Map<IEnumerable<ReviewDTO>>(await _repository.GetAllAsync()));

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
        [Authorize(Roles = Roles.EmployeeOrHigher)]
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
        [Authorize(Roles = Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ReviewDTO>> Get(int id) => 
            Ok(_mapper.Map<ReviewDTO>(await _repository.GetAsync(id)));

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> Create([FromBody] CreateReviewDTO createReviewDTO)
        {
            var review = _mapper.Map<Review>(createReviewDTO);
            if (User.Identity?.IsAuthenticated is true)
            {
                review.UserId = UserId;
                review.Name = User.FindFirst(JwtRegisteredClaimNames.GivenName)?.Value;
            }

            var createdReview = await _repository.CreateAsync(review);
            if (createdReview is null) return UnprocessableEntity();
            return Ok(createdReview.Id);
        }

        /// <summary>
        /// Full update the review
        /// </summary>  
        /// <remarks>
        /// PUT /reviews
        /// {
        ///     id: "1",
        ///     name: "Updated review name",
        ///     price: "2155"
        /// }
        /// </remarks>
        /// <param name="reviewDTO">ReviewDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user tries to update the review that does not belong to him</response>
        [HttpPut]
        [Authorize(Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] ReviewDTO reviewDTO)
        {
            await _repository.UpdateAsync(_mapper.Map<Review>(reviewDTO));
            return NoContent();
        }

        /// <summary>
        /// Partially update the review
        /// </summary>
        /// <remarks>
        /// PATCH /reviews
        /// {
        ///     id: "1",
        ///     name: "Updated review name"
        /// }
        /// </remarks>
        /// <param name="updateReviewDTO">UpdateReviewDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user tries to update the review that does not belong to him</response>
        [HttpPatch]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] UpdateReviewDTO updateReviewDTO)
        {
            var review = await _repository.GetAsync(updateReviewDTO.Id);

            if (User.IsInRole(Roles.User) && review.UserId != UserId)
                return Forbid();

            review.Rating = updateReviewDTO.Rating;
            review.Content = updateReviewDTO.Content;
            review.LastChangeDate = DateTime.Now;

            await _repository.SaveChangesAsync();

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
            Ok(_mapper.Map<IEnumerable<ReviewDTO>>(await _repository.GetReviewsByProductAsync(productId)));

        /// <summary>
        /// Get the reviews enumeration by user id
        /// </summary>
        /// <remarks>
        /// GET /reviews/user/current
        /// </remarks>
        /// <returns>Returns IEnumerable<ReviewDTO></returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("user/{userId:guid}")]
        [Authorize(Roles.EmployeeOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetUserReviews(Guid userId) =>
            Ok(_mapper.Map<IEnumerable<ReviewDTO>>(await _repository.GetUserReviewsAsync(userId)));

        /// <summary>
        /// Get the user's reviews enumeration
        /// </summary>
        /// <remarks>
        /// GET /reviews/user/current
        /// </remarks>
        /// <returns>Returns IEnumerable<ReviewDTO></returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("user/current")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetUserReviews() =>
            Ok(_mapper.Map<IEnumerable<ReviewDTO>>(await _repository.GetUserReviewsAsync(UserId)));

        /// <summary>
        /// Get the order belonging to the current user by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /reviews/user/current/1
        /// </remarks>
        /// <param name="id">Review id (int)</param>
        /// <returns>Returns ReviewDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("user/current/{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ReviewDTO>> GetUserReview(int id) =>
            Ok(_mapper.Map<ReviewDTO>(await _repository.GetUserReviewAsync(id, UserId)));
    }
}
