using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Mapping;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// <response code="404">Not Found</response>
        [HttpGet("exists/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> Exist(int id) =>
            await _repository.ExistsAsync(id) ? Ok(true) : NotFound(false);

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
        /// <response code="404">Not Found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WishlistDTO>> Get(int id)
        {
            var wishlist = await _repository.GetAsync(id);
            if (wishlist is null) return NotFound();

            return Ok(wishlist.ToDTO());
        }

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
        /// <param name="wishlistDTO">WishlistDTO</param>
        /// <returns>Returns entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="422">If the incorrect wishlist DTO was passed</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<int>> Create([FromBody] WishlistDTO wishlistDTO)
        {
            var wishlist = await _repository.CreateAsync(wishlistDTO.FromDTO());
            if (wishlist is null) return UnprocessableEntity();
            return Ok(wishlist.Id);
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
        /// <param name="wishlistDTO">WishlistDTO</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromBody] WishlistDTO wishlistDTO)
        {
            await _repository.UpdateAsync(wishlistDTO.FromDTO());
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
        /// <response code="404">Not Found</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id) =>
            await _repository.DeleteAsync(id) ? NoContent() : NotFound();

        /// <summary>
        /// Get the wishlist by user id
        /// </summary>
        /// <remarks>
        /// GET /wishlists/user/33CCBEBD-FB8A-4919-B727-B2782502E69E
        /// </remarks>
        /// <param name="userId">User id (Guid)</param>
        /// <returns>Returns WishlistDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("user/{userId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WishlistDTO>> GetUserWishlist(Guid userId)
        {
            var wishlist = await _repository.GetUserWishlistAsync(userId);

            if (wishlist == null) return NotFound();

            return Ok(wishlist.ToDTO());
        }

        /// <summary>
        /// Get the current user wishlist
        /// </summary>
        /// <remarks>
        /// GET /wishlists/user/current
        /// </remarks>
        /// <param name="userId">User id (Guid)</param>
        /// <returns>Returns WishlistDTO</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("user/current")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WishlistDTO>> GetUserWishlist()
        {
            var wishlist = await _repository.GetUserWishlistAsync(UserId);

            if (wishlist == null) return NotFound();

            return Ok(wishlist.ToDTO());
        }
    }
}
