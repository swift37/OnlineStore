using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.Wishlist;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Mapping;
using OnlineStore.Domain.Constants;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    public class WishlistsController : BaseController
    {
        private readonly IWishlistsRepository _repository;

        public WishlistsController(IWishlistsRepository repository) =>
            _repository = repository;

        /// <summary>
        /// Get the enumeration of wishlists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /wishlists
        /// </remarks>
        /// <returns>Returns IEnumerable<WishlistDTO></returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<WishlistDTO>>> GetAll() =>
            Ok((await _repository.GetAllAsync()).ToDTO());

        /// <summary>
        /// Get true if wishlist exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /wishlists/exists/1
        /// </remarks>
        /// <param name="id">Wishlist id</param>
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
        /// Get the wishlist by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /wishlists/1
        /// </remarks>
        /// <param name="id">Wishlist id (int)</param>
        /// <returns>Returns WishlistDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet("{id:int}")]
        [Authorize(Roles = Roles.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<WishlistDTO>> Get(int id) => 
            Ok((await _repository.GetAsync(id)).ToDTO());

        /// <summary>
        /// Create a wishlist
        /// </summary>
        /// <remarks>
        /// POST /wishlists
        /// {
        ///     name: "Wishlist name",
        ///     price: "2155"
        /// }
        /// </remarks>
        /// <param name="createWishlistDTO">CreateWishlistDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect wishlist DTO was passed</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> Create([FromBody] CreateWishlistDTO createWishlistDTO)
        {
            var wishlist = createWishlistDTO.FromDTO();
            wishlist.UserId = UserId;

            var createdWishlist = await _repository.CreateAsync(wishlist);
            if (createdWishlist is null) return UnprocessableEntity();
            return Ok(createdWishlist.Id);
        }

        /// <summary>
        /// Update the wishlist
        /// </summary>
        /// <remarks>
        /// PUT /wishlists
        /// {
        ///     name: "Updated wishlist name"
        /// }
        /// </remarks>
        /// <param name="updateWishlistDTO">UpdateWishlistDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user tries to update the wishlist that does not belong to him</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] UpdateWishlistDTO updateWishlistDTO)
        {
            var wishlist = await _repository.GetAsync(updateWishlistDTO.Id);
            if (wishlist.UserId != UserId)
                return Forbid();

            await _repository.UpdateAsync(updateWishlistDTO.FromDTO());
            return NoContent();
        }

        /// <summary>
        /// Delete the wishlist by id
        /// </summary>
        /// <remarks>
        /// DELETE /wishlists/1
        /// </remarks>
        /// <param name="id">Wishlist id (int)</param>
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
        /// Get the wishlist by user id
        /// </summary>
        /// <remarks>
        /// GET /wishlists/user/33CCBEBD-FB8A-4919-B727-B2782502E69E
        /// </remarks>
        /// <param name="userId">User id (Guid)</param>
        /// <returns>Returns WishlistDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user does not have the required access level</response>
        [HttpGet("user/{userId:Guid}")]
        [Authorize(Roles = Roles.ManagerOrHigher)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<WishlistDTO>> GetUserWishlist(Guid userId) => 
            Ok((await _repository.GetUserWishlistAsync(userId)).ToDTO());

        /// <summary>
        /// Get the current user wishlist
        /// </summary>
        /// <remarks>
        /// GET /wishlists/user/current
        /// </remarks>
        /// <param name="userId">User id (Guid)</param>
        /// <returns>Returns WishlistDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("user/current")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<WishlistDTO>> GetUserWishlist() => 
            Ok((await _repository.GetUserWishlistAsync(UserId)).ToDTO());
    }
}
